
import { getUserByEmail, loginUser } from "../api/users";

const AUTH_TOKEN_KEY = "token";
const AUTH_ROLE_KEY = "role";
const AUTH_USER_ID_KEY = "userId";
const AUTH_NAME_KEY = "userName";
const AUTH_PHOTO_KEY = "userPhoto";
const MOCK_ORGANIZER_USER_ID = "ddd4ec10-52b8-44ae-8bd0-6473d37e9257";

function normalizeRole(role) {
  if (role === 1 || role === "1" || role === "User") {
    return "User";
  }

  if (role === 2 || role === "2" || role === "Organizer") {
    return "Organizer";
  }

  return "";
}

export function getStoredRole() {
  return localStorage.getItem(AUTH_ROLE_KEY) ?? "";
}

export function getStoredToken() {
  return localStorage.getItem(AUTH_TOKEN_KEY) ?? "";
}

export function getStoredUserId() {
  return localStorage.getItem(AUTH_USER_ID_KEY) ?? MOCK_ORGANIZER_USER_ID;
}

export function getStoredUserName() {
  return localStorage.getItem(AUTH_NAME_KEY) ?? "";
}

export function getStoredUserPhoto() {
  return localStorage.getItem(AUTH_PHOTO_KEY) ?? "";
}

// seta no localstorage os dados da sessão de autenticação
export function storeAuthSession({ token, role, userId, name, photo }) {
  if (token) {
    localStorage.setItem(AUTH_TOKEN_KEY, token);
  }

  if (role) {
    localStorage.setItem(AUTH_ROLE_KEY, role);
  }

  if (userId) {
    localStorage.setItem(AUTH_USER_ID_KEY, userId);
  }

  if (name) {
    localStorage.setItem(AUTH_NAME_KEY, name);
  }

  if (photo) {
    localStorage.setItem(AUTH_PHOTO_KEY, JSON.stringify(photo));
  }
}

export function clearAuthSession() {
  localStorage.removeItem(AUTH_TOKEN_KEY);
  localStorage.removeItem(AUTH_ROLE_KEY);
  localStorage.removeItem(AUTH_USER_ID_KEY);
  localStorage.removeItem(AUTH_NAME_KEY);
  localStorage.removeItem(AUTH_PHOTO_KEY);
}

export async function loginAndStoreSession({ email, password }) {
  const authResponse = await loginUser({
    email: { value: email },
    password: { value: password },
  });

  storeAuthSession({
    token: authResponse?.token ?? authResponse?.Token ?? "",
    role: "User", // Será atualizado conforme necessário
    userId: authResponse?.id ?? authResponse?.Id ?? "",
    name: authResponse?.name ?? authResponse?.Name ?? "",
    photo: authResponse?.photoProfile ?? authResponse?.PhotoProfile ?? null,
  });

  return authResponse;
}

export function canCreateEvent(role = getStoredRole()) {
  return true;
}
