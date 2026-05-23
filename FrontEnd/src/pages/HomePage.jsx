import "./HomePage.css";
import Header from '../components/Home/Header';
import Footer from "../components/Home/Footer";
import EventCard from "../components/Home/EventCard";

import { events } from "../data/events";

function Home() {
  return (
    <>
      <Header />

      <main className="home">
        <section className="hero">
          <h1>Eventos em Destaque</h1>
          <p>Descubra os melhores eventos da sua cidade</p>
        </section>

        <section className="events-grid">
          {events.map((event) => (
            <EventCard key={event.id} event={event} />
          ))}
        </section>
      </main>

      <Footer />
    </>
  );
}

export default Home;