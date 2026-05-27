import "./HomePage.css";
import { useEffect, useState } from "react";
import { X, MapPin, Calendar, Clock, Ticket } from "lucide-react";
import HeaderUser from '../../components/HeaderUser/HeaderUser';
import Footer from "../../components/Home/Footer";
import EventCard from "../../components/Home/EventCard";
import { getEventById, getEvents, getStateCode } from "../../api/events";
import { setStoredEventId } from "../../utils/eventContext";

const fallbackImage = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f";

const mockEvents = [
  {
    id: "1",
    name: "Festival de Verão",
    description: "Um evento incrível com apresentações musicais, comida de rua e muita diversão. Venha se divertir com amigos e família.",
    city: "São Paulo",
    state: "SP",
    date: "15/07/2026",
    hour: "18:00",
    bannerImage: "https://images.unsplash.com/photo-1511379938547-c1f69b13d835?w=800",
    ticketValue: 80.00,
    totalTicketQuantity: 500,
    street: "Avenida Paulista",
    number: 1000,
    neighborhood: "Bela Vista",
  },
  {
    id: "2",
    name: "Noite Eletrônica",
    description: "A maior festa eletrônica do ano com DJs internacionais. Som de qualidade, luzes impressionantes e ambiente incrível.",
    city: "Rio de Janeiro",
    state: "RJ",
    date: "22/07/2026",
    hour: "20:30",
    bannerImage: "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=800",
    ticketValue: 120.00,
    totalTicketQuantity: 800,
    street: "Rua da Lapa",
    number: 500,
    neighborhood: "Lapa",
  },
  {
    id: "3",
    name: "Samba Sunset",
    description: "Samba ao vivo com vista para o mar. Venha dançar ao som da melhor música brasileira durante o pôr do sol.",
    city: "Belo Horizonte",
    state: "MG",
    date: "28/07/2026",
    hour: "17:00",
    bannerImage: "https://images.unsplash.com/photo-1514525253161-7a46d19cd819?w=800",
    ticketValue: 65.00,
    totalTicketQuantity: 600,
    street: "Avenida Mineirão",
    number: 300,
    neighborhood: "Funcionários",
  },
  {
    id: "4",
    name: "Tech Conference 2026",
    description: "Conferência de tecnologia com palestras de experts da indústria. Aprenda sobre as últimas tendências e inovações.",
    city: "Curitiba",
    state: "PR",
    date: "02/08/2026",
    hour: "09:00",
    bannerImage: "https://images.unsplash.com/photo-1552664730-d307ca884978?w=800",
    ticketValue: 150.00,
    totalTicketQuantity: 1000,
    street: "Centro de Convenções",
    number: 1,
    neighborhood: "Centro",
  },
  {
    id: "5",
    name: "Festival de Gastronomia",
    description: "Mergulhe em uma experiência culinária única com chefs renomados. Deguste pratos especiais e bebidas selecionadas.",
    city: "Salvador",
    state: "BA",
    date: "05/08/2026",
    hour: "19:00",
    bannerImage: "https://images.unsplash.com/photo-1564183346067-c92cdd611de8?w=800",
    ticketValue: 95.00,
    totalTicketQuantity: 400,
    street: "Praça da Republica",
    number: 200,
    neighborhood: "Pelourinho",
  },
];

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
          totalTicketQuantity: event.totalTicketQuantity,
        })));
      } catch (requestError) {
        if (isMounted) {
          setError("");
          setEvents(mockEvents.map((event) => ({
            id: event.id,
            title: event.name,
            city: `${event.city}${event.state ? ` - ${getStateCode(event.state)}` : ""}`,
            date: event.date,
            hour: event.hour,
            image: event.bannerImage || fallbackImage,
            ticketValue: event.ticketValue,
            totalTicketQuantity: event.totalTicketQuantity,
          })));
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
          const mockEvent = mockEvents.find(e => e.id === selectedEvent.id);
          setSelectedEventDetails(mockEvent || selectedEvent);
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
      <HeaderUser />

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