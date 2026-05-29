import "./Register.css";

import { useLocation } from "react-router-dom";
import Header from "../../components/Home/Header";
import RegisterForm from "../../components/RegisterForm/RegisterForm";
import Footer from "../../components/Home/Footer";

function Register() {
  const location = useLocation();
  const isOrganizerRegistration = location.pathname.includes("organizador");

  return (
    <>
      <Header />

      <main className="register-page">
        <RegisterForm isOrganizerRegistration={isOrganizerRegistration} />
      </main>

      <Footer />
    </>
  );
}

export default Register;