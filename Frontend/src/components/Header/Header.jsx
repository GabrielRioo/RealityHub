import "./Header.jsx";

function Header() {
  return (
    <header className="header">
      <h1>RealityHub</h1>

      <nav>
        <a href="#">A Casa</a>
        <a href="#" className="active">
          Votação
        </a>
        <a href="#">Estatísticas</a>
      </nav>
    </header>
  );
}

export default Header;