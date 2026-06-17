using MediatR;

namespace RealityHub.Application.UseCases.Votes.CastVote
{
    // Representa um pedido de ação: "Usuário quer votar no participante X"
    public class CastVoteCommand : IRequest<CastVoteResult>
    {
        public Guid ParticipantId { get; set; }
    }
}
