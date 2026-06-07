const DEFAULT_API_URL = "http://localhost:5202";
const API_URL = (import.meta.env.VITE_API_URL ?? DEFAULT_API_URL).replace(/\/$/, "");

const PURCHASED_TICKETS_KEY = "ingressoja:purchasedTickets";

async function apiRequest(path, options = {}) {
  const response = await fetch(`${API_URL}${path}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...(options.headers ?? {}),
    },
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || "Falha ao comunicar com a API.");
  }

  if (response.status === 204) return null;

  const responseText = await response.text();
  return responseText ? JSON.parse(responseText) : null;
}

export function getTicketsByUserId(userId) {
  return apiRequest(`/tickets/user/${userId}`).then((tickets) =>
    Array.isArray(tickets) ? tickets : []
  );
}

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