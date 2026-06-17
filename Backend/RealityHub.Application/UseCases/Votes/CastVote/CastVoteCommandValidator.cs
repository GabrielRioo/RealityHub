using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.UseCases.Votes.CastVote
{
    public class CastVoteCommandValidator : AbstractValidator<CastVoteCommand>
    {
        public CastVoteCommandValidator()
        {
            RuleFor(x => x.ParticipantId).NotEmpty().WithMessage("ParticipantId é obrigatório");
        }
    }
}
