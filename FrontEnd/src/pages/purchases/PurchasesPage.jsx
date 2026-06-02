import { getTicketsByUserId } from "../../utils/tickets";
import { getEventById } from "../../api/events";
import { getStoredUserId } from "../../utils/auth";

useEffect(() => {
  const userId = getStoredUserId();

  async function loadTickets() {
    try {
      const apiTickets = await getTicketsByUserId(userId);

      const enriched = await Promise.all(
        apiTickets.map(async (t) => {
          try {
            const event = await getEventById(t.eventId);
            return {
              id: t.code,
              code: t.code,
              eventId: t.eventId,
              eventName: event.name ?? "Evento",
              eventDescription: event.description ?? "",
              eventCity: event.city ?? "",
              eventState: event.state ?? "",
              eventStreet: event.street ?? "",
              eventNeighborhood: event.neighborhood ?? "",
              eventNumber: event.number ?? "",
              eventDate: event.date ?? "",
              eventHour: event.hour ?? "",
              bannerImage: event.bannerImage ?? "",
              quantity: 1,
              unitPrice: event.ticketValue ?? 0,
              totalPrice: event.ticketValue ?? 0,
              purchasedAt: new Date().toISOString(),
              status: "Pago",
            };
          } catch {
            return {
              id: t.code,
              code: t.code,
              eventId: t.eventId,
              eventName: "Evento não encontrado",
              status: "Pago",
            };
          }
        })
      );

      setTickets(enriched.length === 0 ? [mockTicket] : enriched);
    } catch {
      setTickets([mockTicket]);
    }
  }

  loadTickets();
}, []);