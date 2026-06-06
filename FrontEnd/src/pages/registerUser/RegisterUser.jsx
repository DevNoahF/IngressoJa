import "./Register.css";

import { useLocation } from "react-router-dom";
import RegisterForm from "../../components/RegisterForm/RegisterForm";
import Footer from "../../components/Home/Footer";

function RegisterUser() {
  const location = useLocation();
  const path = location.pathname.toLowerCase();
  const isOrganizerRegistration = path.includes("organizer") || path.includes("user");
  const roleName = 1

  return (
    <>
      <main className="register-page">
        <RegisterForm isOrganizerRegistration={isOrganizerRegistration} roleName={roleName} />
      </main>
      <Footer />
    </>
  );
}

export default RegisterUser;