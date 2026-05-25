import "./CreateEvent.css";

import Header from "../../components/Home/Header";
import Footer from "../../components/Home/Footer";
import CreateEventForm from "../../components/CreateEventForm/CreateEventForm";

function CreateEvent() {
  return (
    <>
      <Header />

      <main className="create-event-page">
        <CreateEventForm />
      </main>
      <Footer/>
    </>
  );
}

export default CreateEvent;