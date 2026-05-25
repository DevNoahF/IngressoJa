const DEFAULT_API_URL = "http://localhost:5202";

const API_URL = (import.meta.env.VITE_API_URL ?? DEFAULT_API_URL).replace(/\/$/, "");

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
  return request("/events");
}

export function createEvent(payload) {
  return request("/events", {
    method: "POST",
    body: JSON.stringify(payload),
  });
}