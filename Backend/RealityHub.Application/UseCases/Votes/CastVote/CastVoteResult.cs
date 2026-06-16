namespace RealityHub.Application.UseCases.Votes.CastVote
{
    // objeto que representa o resultado da votação
    public class CastVoteResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? SecondsToWait { get; set; }
    }
}
