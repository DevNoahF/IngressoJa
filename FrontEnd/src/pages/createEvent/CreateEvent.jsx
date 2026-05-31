import "./CreateEvent.css";
import HeaderOrganizer from "../../components/headerOrganizer/HeaderOrganizer";
import CreateEventForm from "../../components/CreateEventForm/CreateEventForm";

function CreateEvent() {
  return (
    <>
      <HeaderOrganizer />
      <main className="create-event-page">
        <CreateEventForm />
      </main>
    </>
  );
}

export default CreateEvent;