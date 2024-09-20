import React from "react";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link } from "react-router-dom";
import { useAuth } from "../Context/useAuth";
import Button from "react-bootstrap/Button";

function NavigationBar() {
  const { isLoggedIn, user, logout } = useAuth();
  return (
    <Navbar collapseOnSelect expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand as={Link} to="/">
          The BookDex
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          {isLoggedIn() ? (
            <React.Fragment>
              <Nav className="me-auto">
                <Nav.Link as={Link} to="/cart">
                  Cart
                </Nav.Link>
                <Nav.Link>Library</Nav.Link>
              </Nav>
              <Nav>
                <Navbar.Text className="ml-auto">
                  Welcome, {user?.userName} {"  "}
                </Navbar.Text>
                <Button variant="danger" onClick={logout} className="ml-auto">
                  Logout
                </Button>
              </Nav>
            </React.Fragment>
          ) : (
            <React.Fragment>
              <Nav className="me-auto"></Nav>
              <Nav>
                <Nav.Link as={Link} to="/login" className="ml-auto">
                  Login
                </Nav.Link>
                <Nav.Link as={Link} to="/register" className="ml-auto">
                  Signup
                </Nav.Link>
              </Nav>
            </React.Fragment>
          )}
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default NavigationBar;
