const CURRENT_EVENT_ID_KEY = "currentEventId";

export function getStoredEventId() {
  return localStorage.getItem(CURRENT_EVENT_ID_KEY) ?? "";
}

export function setStoredEventId(eventId) {
  if (eventId) {
    localStorage.setItem(CURRENT_EVENT_ID_KEY, eventId);
  }
}

export function clearStoredEventId() {
  localStorage.removeItem(CURRENT_EVENT_ID_KEY);
}
