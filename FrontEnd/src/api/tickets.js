const DEFAULT_API_URL = "http://localhost:5202";

const API_URL = (import.meta.env.VITE_API_URL ?? DEFAULT_API_URL).replace(/\/$/, "");

async function request(path, options = {}) {
  const response = await fetch(`${API_URL}${path}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...(options.headers ?? {}),
    },
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || "Falha ao comunicar com a API de tickets.");
  }

  if (response.status === 204) {
    return null;
  }

  const responseText = await response.text();
  return responseText ? JSON.parse(responseText) : null;
}

function normalizeTicket(ticket) {
  if (!ticket) {
    return null;
  }

  return {
    code: ticket.code ?? ticket.Code ?? "",
    userId: ticket.userId ?? ticket.UserId ?? "",
  };
}

export function getTicketsByUserId(userId) {
  return request(`/tickets/user/${userId}`).then((tickets) =>
    Array.isArray(tickets) ? tickets.map(normalizeTicket).filter(Boolean) : []
  );
}

export function getAllTickets() {
  return request("/tickets").then((tickets) =>
    Array.isArray(tickets) ? tickets.map(normalizeTicket).filter(Boolean) : []
  );
}