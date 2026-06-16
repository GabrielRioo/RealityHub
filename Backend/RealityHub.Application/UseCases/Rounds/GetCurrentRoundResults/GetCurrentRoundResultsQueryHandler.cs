using MediatR;
using RealityHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.UseCases.Rounds.GetCurrentRoundResults
{
    public class GetCurrentRoundResultsQueryHandler : IRequestHandler<GetCurrentRoundResultsQuery, List<RoundResultDto>>
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IVoteReadRepository _voteReadRepository;

        public GetCurrentRoundResultsQueryHandler(IRoundRepository roundRepository, IVoteReadRepository voteReadRepository)
        {
            _roundRepository = roundRepository;
            _voteReadRepository = voteReadRepository;
        }


        public async Task<List<RoundResultDto>> Handle(GetCurrentRoundResultsQuery request, CancellationToken cancellationToken)
        {
            // Busca o round atual
            var round = await _roundRepository.GetCurrentOpenRoundAsync();

            // Se não existe round aberto, retorna vazio
            if (round is null)
                return new List<RoundResultDto>();

            // Busca os resultados
            return await _voteReadRepository.GetCurrentRoundResultsAsync(round.Id);
        }
    }
}
