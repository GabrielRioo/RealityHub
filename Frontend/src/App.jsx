import { useState } from "react";

import Header from "./components/Header/Header";
import ContestantCard from "./components/ContestantCard/ContestantCard";
import VoteResult from "./components/VoteResult/VoteResult";

import contestantsData from "./data/contestants";

function App() {
  const [contestants, setContestants] = useState(contestantsData);

  const [hasVoted, setHasVoted] = useState(false);

  function handleVote(id) {
    if (hasVoted) return;

    const updatedContestants = contestants.map((contestant) => {
      if (contestant.id === id) {
        return {
          ...contestant,
          votes: contestant.votes + 1,
        };
      }

      return contestant;
    });

    setContestants(updatedContestants);

    setHasVoted(true);
  }

  return (
    <>
      <Header />

      <main className="container">
        <div className="hero">
          <span>Paredão Decisivo</span>

          <h1>Quem deve deixar a casa?</h1>

          <p>Vote no participante que você deseja eliminar.</p>
        </div>

        <section className="cards">
          {contestants.map((contestant) => (
            <ContestantCard
              key={contestant.id}
              contestant={contestant}
              onVote={handleVote}
              hasVoted={hasVoted}
            />
          ))}
        </section>

        {hasVoted && <VoteResult contestants={contestants} />}
      </main>
    </>
  );
}

export default App;