
import { getUserByEmail, loginUser } from "../api/users";

const AUTH_TOKEN_KEY = "token";
const AUTH_ROLE_KEY = "role";
const AUTH_USER_ID_KEY = "userId";
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

export function storeAuthSession({ token, role, userId }) {
  if (token) {
    localStorage.setItem(AUTH_TOKEN_KEY, token);
  }

  if (role) {
    localStorage.setItem(AUTH_ROLE_KEY, role);
  }

  if (userId) {
    localStorage.setItem(AUTH_USER_ID_KEY, userId);
  }
}

export function clearAuthSession() {
  localStorage.removeItem(AUTH_TOKEN_KEY);
  localStorage.removeItem(AUTH_ROLE_KEY);
  localStorage.removeItem(AUTH_USER_ID_KEY);
}

export async function loginAndStoreSession({ email, password }) {
  const authResponse = await loginUser({
    email: { value: email },
    password: { value: password },
  });

  const user = await getUserByEmail(email);
  const role = normalizeRole(user?.role ?? user?.Role);

  storeAuthSession({
    token: authResponse?.token ?? authResponse?.Token ?? "",
    role,
    userId: user?.id ?? user?.Id ?? "",
  });

  return { authResponse, user };
}

export function canCreateEvent(role = getStoredRole()) {
  return true;
}
