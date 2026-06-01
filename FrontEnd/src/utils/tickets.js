const PURCHASED_TICKETS_KEY = "ingressoja:purchasedTickets";

function createTicketCode() {
  const randomPart = Math.random().toString(36).slice(2, 10).toUpperCase();
  return `TICKET-${Date.now().toString(36).toUpperCase()}-${randomPart}`;
}

function createTicketId() {
  if (typeof crypto !== "undefined" && typeof crypto.randomUUID === "function") {
    return crypto.randomUUID();
  }

  return `${Date.now()}-${Math.random().toString(36).slice(2)}`;
}

function normalizeTicket(ticket) {
  return {
    id: ticket.id ?? createTicketId(),
    code: ticket.code ?? createTicketCode(),
    eventId: ticket.eventId ?? "",
    eventName: ticket.eventName ?? "Evento",
    eventDescription: ticket.eventDescription ?? "",
    eventCity: ticket.eventCity ?? "",
    eventState: ticket.eventState ?? "",
    eventStreet: ticket.eventStreet ?? "",
    eventNeighborhood: ticket.eventNeighborhood ?? "",
    eventNumber: ticket.eventNumber ?? "",
    eventDate: ticket.eventDate ?? "",
    eventHour: ticket.eventHour ?? "",
    bannerImage: ticket.bannerImage ?? "",
    quantity: Number(ticket.quantity ?? 1),
    unitPrice: Number(ticket.unitPrice ?? 0),
    totalPrice: Number(ticket.totalPrice ?? 0),
    purchasedAt: ticket.purchasedAt ?? new Date().toISOString(),
    status: ticket.status ?? "Confirmado",
  };
}

function readTicketsFromStorage() {
  if (typeof window === "undefined") {
    return [];
  }

  try {
    const rawValue = window.localStorage.getItem(PURCHASED_TICKETS_KEY);
    const parsedValue = rawValue ? JSON.parse(rawValue) : [];

    if (!Array.isArray(parsedValue)) {
      return [];
    }

    return parsedValue.map(normalizeTicket);
  } catch {
    return [];
  }
}

function writeTicketsToStorage(tickets) {
  if (typeof window === "undefined") {
    return;
  }

  window.localStorage.setItem(PURCHASED_TICKETS_KEY, JSON.stringify(tickets));
}

export function getPurchasedTickets() {
  return readTicketsFromStorage();
}

export function addPurchasedTicket(ticketData) {
  const currentTickets = readTicketsFromStorage();
  const ticket = normalizeTicket(ticketData);
  const nextTickets = [ticket, ...currentTickets];

  writeTicketsToStorage(nextTickets);

  return ticket;
}