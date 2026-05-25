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
    throw new Error(errorText || "Falha ao comunicar com a API de usuários.");
  }

  if (response.status === 204) {
    return null;
  }

  const responseText = await response.text();
  return responseText ? JSON.parse(responseText) : null;
}

export function registerUser(payload) {
  return request("/api/users/register", {
    method: "POST",
    body: JSON.stringify(payload),
  });
}

export function registerOrganizer(payload) {
  return request("/api/users/register/organizer", {
    method: "POST",
    body: JSON.stringify(payload),
  });
}

export function loginUser(payload) {
  return request("/api/auth", {
    method: "POST",
    body: JSON.stringify(payload),
  });
}

export function getUserByEmail(email) {
  return request(`/api/users?email=${encodeURIComponent(email)}`);
}
