import "./OrganizerEvents.css";

import { useEffect, useMemo, useState } from "react";
import HeaderOrganizer from "../../components/headerOrganizer/HeaderOrganizer";
import OrganizerEventCard from "../../components/OrganizerEvents/OrganizerEventCard";
import { getEvents, getStateCode } from "../../api/events";
import { getStoredUserId } from "../../utils/auth";

const fallbackImage = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f";

const mockEvents = [
  {
    id: 1,
    name: "Festival de Rock 2026",
    description: "Evento principal do calendário do organizer.",
    bannerImage: fallbackImage,
    location: "São Paulo - SP",
    formattedDate: "15/07/2026",
    hour: "18:00",
    ticketValue: "150,00",
    totalTicketQuantity: 450,
    statusLabel: "Próximo",
  },
  {
    id: 2,
    name: "Noite Eletrônica",
    description: "Uma segunda opção de evento para visualização.",
    bannerImage: fallbackImage,
    location: "Campinas - SP",
    formattedDate: "22/08/2026",
    hour: "21:30",
    ticketValue: "90,00",
    totalTicketQuantity: 320,
    statusLabel: "Próximo",
  },
  {
    id: 3,
    name: "Festival de Verão",
    description: "Mock com imagem e dados de apoio para a página.",
    bannerImage: fallbackImage,
    location: "Santos - SP",
    formattedDate: "05/09/2026",
    hour: "17:00",
    ticketValue: "75,00",
    totalTicketQuantity: 280,
    statusLabel: "Próximo",
  },
  {
    id: 4,
    name: "Experiência Urbana",
    description: "Mais um card para preencher a grade visual.",
    bannerImage: fallbackImage,
    location: "Ribeirão Preto - SP",
    formattedDate: "12/10/2026",
    hour: "19:00",
    ticketValue: "60,00",
    totalTicketQuantity: 190,
    statusLabel: "Próximo",
  },
];

function formatDate(value) {
  if (!value) {
    return "Data não informada";
  }

  const date = new Date(value);

  if (Number.isNaN(date.getTime())) {
    return value;
  }

  return new Intl.DateTimeFormat("pt-BR", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  }).format(date);
}

function getEventDateStatus(event) {
  if (!event.date) {
    return "upcoming";
  }

  const eventDate = new Date(`${event.date}${event.hour ? `T${event.hour}` : "T00:00"}`);

  if (Number.isNaN(eventDate.getTime())) {
    return "upcoming";
  }

  return eventDate >= new Date() ? "upcoming" : "finished";
}

function normalizeEvent(event) {
  const stateCode = getStateCode(event.state) || event.state || "";
  const cityLabel = [event.city, stateCode].filter(Boolean).join(stateCode && event.city ? " - " : "");
  const totalTicketQuantity = Number(event.totalTicketQuantity ?? event.totalTickets ?? 0);
  const ticketValue = Number(event.ticketValue ?? 0);
  const status = getEventDateStatus(event);

  return {
    id: event.id,
    name: event.name ?? event.title ?? "Evento sem nome",
    description: event.description ?? "",
    bannerImage: event.bannerImage || event.image || fallbackImage,
    location: cityLabel || "Localização não informada",
    formattedDate: formatDate(event.date),
    date: event.date,
    hour: event.hour,
    ticketValue: ticketValue.toFixed(2).replace(".", ","),
    totalTicketQuantity: totalTicketQuantity > 0 ? totalTicketQuantity : "-",
    capacityLabel: totalTicketQuantity > 0 ? totalTicketQuantity.toLocaleString("pt-BR") : "-",
    status,
    statusLabel: status === "finished" ? "Encerrado" : "Próximo",
    organizerId: String(event.userId ?? event.organizerId ?? event.ownerId ?? ""),
  };
}

function OrganizerEvents() {
  const organizerId = getStoredUserId().trim();
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

        const normalizedEvents = (Array.isArray(response) ? response : [])
          .map(normalizeEvent)
          .filter((event) => !organizerId || event.organizerId === organizerId);

        setEvents(normalizedEvents);
      } catch (requestError) {
        if (isMounted) {
          setError(requestError instanceof Error ? requestError.message : "Não foi possível carregar os eventos do organizer.");
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
  }, [organizerId]);

  const visibleEvents = useMemo(() => {
    const mappedEvents = events.slice(0, 4);
    return mappedEvents.length > 0 ? mappedEvents : mockEvents;
  }, [events]);

  return (
    <div className="organizer-events-page">
      <HeaderOrganizer />

      <main className="organizer-events-main">
        <section className="organizer-events-grid" aria-live="polite">
          {error ? <p className="organizer-events-state error">{error}</p> : null}

          {visibleEvents.map((event) => (
            <OrganizerEventCard key={event.id} event={event} />
          ))}
        </section>
      </main>
    </div>
  );
}

export default OrganizerEvents;