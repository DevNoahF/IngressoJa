const DEFAULT_API_URL = "http://localhost:5202";

const API_URL = (import.meta.env.VITE_API_URL ?? DEFAULT_API_URL).replace(/\/$/, "");

function unwrapValue(field) {
  if (field && typeof field === "object" && "value" in field) {
    return field.value;
  }

  return field ?? "";
}

function normalizeEvent(event) {
  if (!event) {
    return null;
  }

  return {
    id: event.id ?? event.EventId ?? "",
    name: unwrapValue(event.name ?? event.Name),
    description: unwrapValue(event.description ?? event.Description),
    street: unwrapValue(event.street ?? event.Street ?? event.streetName ?? event.StreetName),
    neighborhood: unwrapValue(event.neighborhood ?? event.Neighborhood),
    city: unwrapValue(event.city ?? event.City),
    number: event.number ?? event.Number ?? 0,
    state: event.state ?? event.State ?? 0,
    date: event.date ?? event.Date ?? "",
    hour: event.hour ?? event.Hour ?? "",
    ticketValue: unwrapValue(event.ticketValue ?? event.TicketValue) ?? 0,
    totalTicketQuantity: unwrapValue(event.totalTicketQuantity ?? event.TotalTicketQuantity) ?? 0,
    bannerImage: unwrapValue(event.bannerImage ?? event.BannerImage),
    userId: event.userId ?? event.UserId ?? "",
    status: event.status ?? event.Status ?? 1,
    createdAt: event.createdAt ?? event.CreatedAt ?? null,
    updatedAt: event.updatedAt ?? event.UpdatedAt ?? null,
  };
}

function wrapValue(value) {
  return { value };
}

function buildCreatePayload(payload) {
  return {
    name: wrapValue(payload.name),
    description: wrapValue(payload.description),
    street: wrapValue(payload.street),
    neighborhood: wrapValue(payload.neighborhood),
    city: wrapValue(payload.city),
    number: Number(payload.number),
    state: Number(payload.state),
    date: payload.date,
    hour: payload.hour,
    ticketValue: wrapValue(Number(payload.ticketValue)),
    totalTicketQuantity: wrapValue(Number(payload.totalTicketQuantity)),
    bannerImage: wrapValue(payload.bannerImage),
    userId: payload.userId,
    status: Number(payload.status ?? 1),
  };
}

function buildUpdatePayload(payload) {
  return {
    name: wrapValue(payload.name),
    description: wrapValue(payload.description),
    street: wrapValue(payload.street),
    neighborhood: wrapValue(payload.neighborhood),
    city: wrapValue(payload.city),
    number: Number(payload.number),
    state: Number(payload.state),
    date: payload.date,
    hour: payload.hour,
    ticketValue: wrapValue(Number(payload.ticketValue)),
    totalTicketQuantity: wrapValue(Number(payload.totalTicketQuantity)),
    bannerImage: wrapValue(payload.bannerImage),
    status: payload.status ? Number(payload.status) : undefined,
  };
}

export const statesOptions = [
  { value: 1, code: "AC", name: "Acre" },
  { value: 2, code: "AL", name: "Alagoas" },
  { value: 3, code: "AP", name: "Amapá" },
  { value: 4, code: "AM", name: "Amazonas" },
  { value: 5, code: "BA", name: "Bahia" },
  { value: 6, code: "CE", name: "Ceará" },
  { value: 7, code: "DF", name: "Distrito Federal" },
  { value: 8, code: "ES", name: "Espírito Santo" },
  { value: 9, code: "GO", name: "Goiás" },
  { value: 10, code: "MA", name: "Maranhão" },
  { value: 11, code: "MT", name: "Mato Grosso" },
  { value: 12, code: "MS", name: "Mato Grosso do Sul" },
  { value: 13, code: "MG", name: "Minas Gerais" },
  { value: 14, code: "PA", name: "Pará" },
  { value: 15, code: "PB", name: "Paraíba" },
  { value: 16, code: "PR", name: "Paraná" },
  { value: 17, code: "PE", name: "Pernambuco" },
  { value: 18, code: "PI", name: "Piauí" },
  { value: 19, code: "RJ", name: "Rio de Janeiro" },
  { value: 20, code: "RN", name: "Rio Grande do Norte" },
  { value: 21, code: "RS", name: "Rio Grande do Sul" },
  { value: 22, code: "RO", name: "Rondônia" },
  { value: 23, code: "RR", name: "Roraima" },
  { value: 24, code: "SC", name: "Santa Catarina" },
  { value: 25, code: "SP", name: "São Paulo" },
  { value: 26, code: "SE", name: "Sergipe" },
  { value: 27, code: "TO", name: "Tocantins" },
];

function getStateOption(stateValue) {
  return statesOptions.find((option) => option.value === Number(stateValue));
}

export function getStateCode(stateValue) {
  return getStateOption(stateValue)?.code ?? "";
}

export function getStateLabel(stateValue) {
  const option = getStateOption(stateValue);
  return option ? `${option.code} - ${option.name}` : "";
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
    throw new Error(errorText || "Falha ao comunicar com a API de eventos.");
  }

  if (response.status === 204) {
    return null;
  }

  const responseText = await response.text();
  return responseText ? JSON.parse(responseText) : null;
}

export function getEvents() {
  return request("/events").then((events) =>
    Array.isArray(events) ? events.map(normalizeEvent) : []
  );
}

export function getEventsByOrganizerId(organizerId) {
  return request(`/events/organizer/${organizerId}`).then((events) =>
    Array.isArray(events) ? events.map(normalizeEvent) : []
  );
}

export function getEventById(eventId) {
  return request(`/events/${eventId}`).then(normalizeEvent);
}

export function createEvent(payload) {
  return request("/events", {
    method: "POST",
    body: JSON.stringify(buildCreatePayload(payload)),
  });
}

export function updateEvent(eventId, payload) {
  return request(`/events/${eventId}`, {
    method: "PATCH",
    body: JSON.stringify(buildUpdatePayload(payload)),
  }).then(normalizeEvent);
}

export function changeEventStatus(eventId, status) {
  return request(`/events/${eventId}`, {
    method: "PATCH",
    body: JSON.stringify({ status: Number(status) }),
  }).then((response) => (response ? normalizeEvent(response) : response));
}

export function deleteEvent(eventId) {
  return request(`/events/${eventId}`, {
    method: "DELETE",
  });
}

export { normalizeEvent };