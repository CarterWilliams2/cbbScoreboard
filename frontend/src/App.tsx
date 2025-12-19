import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import ScoresPage from "./pages/ScoresPage";
import LeadersPage from "./pages/LeadersPage";


function App() {
  return (
    <BrowserRouter>
    <div style={{ padding: 20 }}>
      <nav style={{ marginBottom: 20 }}>
        <Link to="/" style={{ marginRight: 12}}>Scores</Link>
        <Link to="/leaders">Leaders</Link>
      </nav>

      <Routes>
        <Route path="/" element={<ScoresPage />} />
        <Route path="/leaders" element={<LeadersPage />} />
      </Routes>
    </div>
    </BrowserRouter>
  )
}

export default App;
