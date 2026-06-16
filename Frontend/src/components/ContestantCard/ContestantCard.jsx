import "./ContestantCard.css";

function ContestantCard({ contestant, onVote, hasVoted }) {
  return (
    <div className="card">
      <img src={contestant.image} alt={contestant.name} />

      <div className="overlay">
        <span>{contestant.category}</span>

        <h2>{contestant.name}</h2>

        <button onClick={() => onVote(contestant.id)} disabled={hasVoted}>
          {hasVoted ? "Voto registrado" : "Selecionar"}
        </button>
      </div>
    </div>
  );
}

export default ContestantCard;