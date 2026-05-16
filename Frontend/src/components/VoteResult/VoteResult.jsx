import "./VoteResult.css";

function VoteResult({ contestants }) {
  const totalVotes = contestants.reduce(
    (acc, contestant) => acc + contestant.votes,
    0
  );

  return (
    <div className="results">
      <h2>Resultado Parcial</h2>

      {contestants.map((contestant) => {
        const percentage =
          totalVotes > 0
            ? ((contestant.votes / totalVotes) * 100).toFixed(1)
            : 0;

        return (
          <div key={contestant.id} className="result-item">
            <div className="result-info">
              <span>{contestant.name}</span>
              <span>{percentage}%</span>
            </div>

            <div className="bar">
              <div
                className="fill"
                style={{ width: `${percentage}%` }}
              ></div>
            </div>
          </div>
        );
      })}

      <p className="total">Total de votos: {totalVotes}</p>
    </div>
  );
}

export default VoteResult;