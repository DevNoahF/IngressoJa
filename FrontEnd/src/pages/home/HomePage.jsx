import "./HomePage.css";
<<<<<<< HEAD:FrontEnd/src/pages/HomePage.jsx
import { useEffect, useState } from "react";
import { X, MapPin, Calendar, Clock, Ticket } from "lucide-react";
import Header from '../components/Home/Header';
import Footer from "../components/Home/Footer";
import EventCard from "../components/Home/EventCard";
import { getEventById, getEvents, getStateCode } from "../api/events";
import { setStoredEventId } from "../utils/eventContext";

const fallbackImage = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f";
=======
import Header from '../../components/Home/Header';
import Footer from "../../components/Home/Footer";
import EventCard from "../../components/Home/EventCard";

import { events } from "../../data/events";
>>>>>>> 0b47d1e94059111bd8e0c7755a65a6aa01aeb292:FrontEnd/src/pages/home/HomePage.jsx

function Home() {
  const [events, setEvents] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [selectedEventDetails, setSelectedEventDetails] = useState(null);
  const [isDetailLoading, setIsDetailLoading] = useState(false);
  const [detailError, setDetailError] = useState("");
  const [paymentMessage, setPaymentMessage] = useState("");

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
          setEvents([]);
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

  useEffect(() => {
    if (!selectedEvent) {
      setSelectedEventDetails(null);
      setDetailError("");
      setIsDetailLoading(false);
      return;
    }

    let isMounted = true;

    async function loadEventDetails() {
      try {
        setIsDetailLoading(true);
        setDetailError("");

        const response = await getEventById(selectedEvent.id);

        if (!isMounted) {
          return;
        }

        setSelectedEventDetails(response);
      } catch (requestError) {
        if (isMounted) {
          setSelectedEventDetails(selectedEvent);
          setDetailError("");
        }
      } finally {
        if (isMounted) {
          setIsDetailLoading(false);
        }
      }
    }

    loadEventDetails();

    return () => {
      isMounted = false;
    };
  }, [selectedEvent]);

  function handleCloseModal() {
    setSelectedEvent(null);
    setPaymentMessage("");
  }

  function handlePayment() {
    const currentEvent = selectedEventDetails ?? selectedEvent;
    const eventId = selectedEvent?.id ?? currentEvent?.id;

    if (!eventId) {
      return;
    }

    setStoredEventId(String(eventId));
    setPaymentMessage("Evento selecionado para pagamento.");
  }

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
            <EventCard key={event.id} event={event} onReadMore={setSelectedEvent} />
          ))}
        </section>
      </main>

      {selectedEvent ? (
        <div className="event-modal-backdrop" onClick={handleCloseModal} role="presentation">
          <div
            className="event-modal"
            role="dialog"
            aria-modal="true"
            aria-label={`Detalhes do evento ${selectedEvent.title}`}
            onClick={(event) => event.stopPropagation()}
          >
            <button type="button" className="event-modal-close" onClick={handleCloseModal} aria-label="Fechar modal">
              <X size={18} />
            </button>

            {isDetailLoading ? (
              <p className="status-message">Carregando detalhes...</p>
            ) : detailError ? (
              <p className="status-message error">{detailError}</p>
            ) : (selectedEventDetails ?? selectedEvent) ? (
              <>
                <h2>{(selectedEventDetails ?? selectedEvent).name ?? (selectedEventDetails ?? selectedEvent).title}</h2>

                <img
                  className="event-modal-image"
                  src={(selectedEventDetails ?? selectedEvent).bannerImage || (selectedEventDetails ?? selectedEvent).image || selectedEvent.image}
                  alt={(selectedEventDetails ?? selectedEvent).name ?? (selectedEventDetails ?? selectedEvent).title}
                />

                <p className="event-modal-description">{(selectedEventDetails ?? selectedEvent).description}</p>

                <div className="event-modal-details">
                  <span>
                    <MapPin size={16} />
                    {(selectedEventDetails ?? selectedEvent).street}, {(selectedEventDetails ?? selectedEvent).number} - {(selectedEventDetails ?? selectedEvent).neighborhood}
                  </span>

                  <span>
                    <Calendar size={16} />
                    {(selectedEventDetails ?? selectedEvent).date}
                  </span>

                  <span>
                    <Clock size={16} />
                    {(selectedEventDetails ?? selectedEvent).hour}
                  </span>

                  <span>
                    <Ticket size={16} />
                    R$ {Number((selectedEventDetails ?? selectedEvent).ticketValue).toFixed(2)} por ingresso
                  </span>
                </div>

                {paymentMessage ? <p className="event-modal-payment-message">{paymentMessage}</p> : null}

                <button type="button" className="event-modal-payment-btn" onClick={handlePayment}>
                  Fazer Pagamento
                </button>
              </>
            ) : null}
          </div>
        </div>
      ) : null}

      <Footer />
    </>
  );
}

export default Home;