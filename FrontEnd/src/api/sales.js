const DEFAULT_API_URL = "http://localhost:5202";

const API_URL = (import.meta.env.VITE_API_URL ?? DEFAULT_API_URL).replace(/\/$/, "");

function normalizeSale(sale) {
  if (!sale) {
    return null;
  }

  return {
    id: sale.id ?? sale.Id ?? 0,
    userId: sale.userId ?? sale.UserId ?? "",
    eventId: sale.eventId ?? sale.EventId ?? "",
    ticketId: sale.ticketId ?? sale.TicketId ?? null,
    selectedTicketsUser: sale.selectedTicketsUser ?? sale.SelectedTicketsUser ?? 0,
    totalPrice: sale.totalPrice ?? sale.TotalPrice ?? 0,
    saleStatus: sale.saleStatus ?? sale.SaleStatus ?? "",
    createdAt: sale.createdAt ?? sale.CreatedAt ?? null,
  };
}

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
    throw new Error(errorText || "Falha ao comunicar com a API de vendas.");
  }

  if (response.status === 204) {
    return null;
  }

  const responseText = await response.text();
  return responseText ? JSON.parse(responseText) : null;
}

export function createSale({ userId, eventId, selectedTicketsUser }) {
  return request("/sales", {
    method: "POST",
    body: JSON.stringify({
      userId,
      eventId,
      selectedTicketsUser: Number(selectedTicketsUser),
    }),
  }).then(normalizeSale);
}

export function approveSale(saleId) {
  return request(`/sales/${saleId}/status`, {
    method: "PATCH",
  }).then(normalizeSale);
}
