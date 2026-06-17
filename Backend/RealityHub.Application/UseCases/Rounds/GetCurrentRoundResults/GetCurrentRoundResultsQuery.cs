using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.UseCases.Rounds.GetCurrentRoundResults
{
    public class GetCurrentRoundResultsQuery : IRequest<List<RoundResultDto>>
    {
    }
}
