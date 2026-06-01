import "./HomePage.css";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { X, MapPin, Calendar, Clock, Ticket } from "lucide-react";
import HeaderUser from '../../components/HeaderUser/HeaderUser';
import Footer from "../../components/Home/Footer";
import EventCard from "../../components/Home/EventCard";
import OrganizerEventCard from "../../components/OrganizerEvents/OrganizerEventCard";
import { getEventById, getEvents, getStateCode } from "../../api/events";
import { setStoredEventId } from "../../utils/eventContext";

const fallbackImage = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f";

function isGuid(value) {
  return /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(value);
}



const PERMANENT_EVENT = {
  id: "permanent-semanca-1",
  name: "Show da SeManca e SeMata",
  description:
    "Uma noite inesquecível onde a SeManca toca trompete com os pés e a SeMata ensina passos de dança proibidos até pela física. Riso garantido ou SeMata direto para sua casa.",
  city: "Cidade Imaginária",
  state: "ZZ",
  date: "31/12/2026",
  hour: "23:59",
  bannerImage: "https://i.pinimg.com/736x/c5/53/79/c55379996a160a72d08150c3b05db17d.jpg",
  ticketValue: 99.9,
  totalTicketQuantity: 420,
  street: "Rua dos Tropeços",
  number: 13,
  neighborhood: "Vila do Riso",
};

// No mockEvents fallback anymore; when the API fails we show an empty list

function Home() {
  const navigate = useNavigate();
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
          totalTicketQuantity: event.totalTicketQuantity,
        })));
      } catch (requestError) {
        if (isMounted) {
          setError("");
          // No remote events available and no local mock - show empty list
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
          // If the selected event is the permanent demo event, use it as fallback
          if (selectedEvent && selectedEvent.id === PERMANENT_EVENT.id) {
            setSelectedEventDetails(PERMANENT_EVENT);
          } else {
            setSelectedEventDetails(selectedEvent);
          }
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

    if (!isGuid(String(eventId))) {
      setPaymentMessage("Este evento e demonstrativo. Escolha um evento cadastrado para comprar ingresso.");
      return;
    }

    setStoredEventId(String(eventId));
    setPaymentMessage("Evento selecionado para pagamento.");
    navigate("/user/payment");
  }

  return (
    <>
      <HeaderUser />

      <main className="home">
        <section className="hero">
          <h1>Eventos em Destaque</h1>
          <p>Descubra os melhores eventos da sua cidade</p>
        </section>

        <section className="permanent-event-section">
          <h2>Evento permanente</h2>
          <EventCard
            key={PERMANENT_EVENT.id}
            event={{
              id: PERMANENT_EVENT.id,
              title: PERMANENT_EVENT.name,
              city: `${PERMANENT_EVENT.city} - ${PERMANENT_EVENT.state}`,
              date: PERMANENT_EVENT.date,
              hour: PERMANENT_EVENT.hour,
              image: PERMANENT_EVENT.bannerImage,
              ticketValue: PERMANENT_EVENT.ticketValue,
              totalTicketQuantity: PERMANENT_EVENT.totalTicketQuantity,
            }}
            onReadMore={setSelectedEvent}
          />
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
                    {Number((selectedEventDetails ?? selectedEvent).totalTicketQuantity).toLocaleString('pt-BR')} total de ingressos
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
