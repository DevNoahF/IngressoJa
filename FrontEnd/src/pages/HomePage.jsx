import "./HomePage.css";
import { useEffect, useState } from "react";
import Header from '../components/Home/Header';
import Footer from "../components/Home/Footer";
import EventCard from "../components/Home/EventCard";
import { getEvents, getStateCode } from "../api/events";

const fallbackImage = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f";

function Home() {
  const [events, setEvents] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    let isMounted = true;

    async function loadEvents() {
      try {
        setIsLoading(true);
        setError("");

        const response = await getEvents();
        if (!isMounted) {
          return;
        }

        setEvents(response.map((event) => ({
          id: event.id,
          title: event.name,
          city: `${event.city}${event.state ? ` - ${getStateCode(event.state)}` : ""}`,
          date: event.date,
          hour: event.hour,
          image: event.bannerImage || fallbackImage,
          ticketValue: event.ticketValue,
        })));
      } catch (requestError) {
        if (isMounted) {
          setError(requestError instanceof Error ? requestError.message : "Não foi possível carregar os eventos.");
        }
      } finally {
        if (isMounted) {
          setIsLoading(false);
        }
      }
    }

    loadEvents();

    return () => {
      isMounted = false;
    };
  }, []);

  return (
    <>
      <Header />

      <main className="home">
        <section className="hero">
          <h1>Eventos em Destaque</h1>
          <p>Descubra os melhores eventos da sua cidade</p>
        </section>

        <section className="events-grid">
          {isLoading ? <p className="status-message">Carregando eventos...</p> : null}
          {error ? <p className="status-message error">{error}</p> : null}
          {!isLoading && !error && events.length === 0 ? (
            <p className="status-message">Nenhum evento encontrado.</p>
          ) : null}
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