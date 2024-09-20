import "bootstrap/dist/css/bootstrap.min.css";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./App.css";
import { UserProvider } from "./Context/useAuth";
import { Outlet } from "react-router";
import Body from "./layout/Body";
import NavigationBar from "./layout/NavigationBar";

function App() {
  return (
    <div className="App">
      <UserProvider>
        <NavigationBar />
        <Body />
        <ToastContainer />
      </UserProvider>
    </div>
  );
}

export default App;
