
import { getUserByEmail, loginUser } from "../api/users";

const AUTH_TOKEN_KEY = "token";
const AUTH_ROLE_KEY = "role";
const AUTH_USER_ID_KEY = "userId";
const AUTH_NAME_KEY = "userName";
const AUTH_PHOTO_KEY = "userPhoto";

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
  return localStorage.getItem(AUTH_USER_ID_KEY);
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

  console.log("Login response:", authResponse);
  console.log("Role from response:", authResponse?.role);
  
  const normalizedRole = normalizeRole(authResponse?.role ?? authResponse?.Role ?? "User");
  console.log("Normalized role:", normalizedRole);

  storeAuthSession({
    token: authResponse?.token ?? authResponse?.Token ?? "",
    role: normalizedRole,
    userId: authResponse?.id ?? authResponse?.Id ?? "",
    name: authResponse?.firstName ?? authResponse?.FirstName ?? authResponse?.fistName ?? authResponse?.FistName ?? "",
    photo: authResponse?.photoProfile ?? authResponse?.PhotoProfile ?? null,
  });

  return authResponse;
}

export function canCreateEvent(role = getStoredRole()) {
  return true;
}
