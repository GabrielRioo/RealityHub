using MediatR;
using RealityHub.Application.Interfaces;
using RealityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.UseCases.Votes.CastVote
{
    public class CastVoteCommandHandler : IRequestHandler<CastVoteCommand, CastVoteResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRequestMetadataService _requestMetadataService;
        private readonly IRoundRepository _roundRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IRoundParticipantRepository _roundParticipantRepository;
        private readonly IVoteRepository _voterRepository;
        private readonly IVoteAttemptLogRepository _voteAttemptLogRepository;

        public CastVoteCommandHandler(
            ICurrentUserService currentUserService,
            IRequestMetadataService requestMetadataService,
            IRoundRepository roundRepository,
            IParticipantRepository participantRepository,
            IRoundParticipantRepository roundParticipantRepository,
            IVoteRepository voterRepository,
            IVoteAttemptLogRepository voteAttemptLogRepository)
        {
            _currentUserService = currentUserService;
            _requestMetadataService = requestMetadataService;
            _roundRepository = roundRepository;
            _participantRepository = participantRepository;
            _roundParticipantRepository = roundParticipantRepository;
            _voterRepository = voterRepository;
            _voteAttemptLogRepository = voteAttemptLogRepository;
        }

        public async Task<CastVoteResult> Handle(CastVoteCommand request, CancellationToken cancellationToken)
        {
            // Obter quem está votando
            var userId = _currentUserService.GetUserId();

            // Obter dados do request (anti-bot)
            var ip = _requestMetadataService.GetIpAddress();
            var userAgent = _requestMetadataService.GetUserAgent();

            // Buscar round aberto
            #region GetRound
            var round = await _roundRepository.GetCurrentOpenRoundAsync();

            if (round is null)
            {
                await RegisterAttempt(userId, ip, userAgent, false, "Não tem round aberto.");

                return new CastVoteResult
                {
                    Success = false,
                    Message = "Não tem round aberto neste momento."
                };
            }
            #endregion

            // Validar se o participante existe e está ativo.
            #region GetActiveParticipant
            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId);

            if (participant is null || participant.IsActive == false)
            {
                await RegisterAttempt(userId, ip, userAgent, false, "Participante Inválido");

                return new CastVoteResult
                {
                    Success = false,
                    Message = "Participante Inválido."
                };
            }
            #endregion

            // Validar se o participante está no round.
            #region IsParticipantInRound
            var isParticipantInRound = await _roundParticipantRepository.ExistisAsync(round.Id, participant.Id);

            if (!isParticipantInRound)
            {
                await RegisterAttempt(userId, ip, userAgent, false, "Este participante não está neste round");

                return new CastVoteResult
                {
                    Success = false,
                    Message = "Este participante não está neste round atual."
                };
            }
            #endregion

            // Verificar cooldown (2 minutos)
            #region Cooldown
            var lastVoteTime = await _voterRepository.GetLastVoteTimeAsync(userId, round.Id);

            if (lastVoteTime.HasValue)
            {
                var secondsPassed = (DateTime.UtcNow - lastVoteTime.Value).TotalSeconds;
                var cooldownSeconds = 120;

                if (secondsPassed < cooldownSeconds)
                {
                    var remaining = cooldownSeconds - (int)secondsPassed;

                    await RegisterAttempt(userId, ip, userAgent, false, "Cooldown ativado");

                    return new CastVoteResult
                    {
                        Success = false,
                        Message = "Você deve esperar antes de votar novamente.",
                        SecondsToWait = remaining
                    };
                }
            }
            #endregion

            // Criar voto
            #region AddVote
            var vote = new Vote
            {
                Id = Guid.NewGuid(),
                RoundId = round.Id,
                ParticipantId = participant.Id,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _voterRepository.AddAsync(vote);
            #endregion

            // Registrar tentativa de voto (sucesso)
            await RegisterAttempt(userId, ip, userAgent, true, "Voto registrado");

            return new CastVoteResult
            {
                Success = true,
                Message = "Voto registrado com sucesso."
            };
        }
        private async Task RegisterAttempt(Guid userId, string ip, string userAgent, bool success, string reason)
        {
            var log = new VoteAttemptLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                IpAdress = ip,
                UserAgent = userAgent,
                Success = success,
                Reason = reason,
                CreatedAt = DateTime.UtcNow
            };

            await _voteAttemptLogRepository.AddAsync(log);
        }
    }

}
