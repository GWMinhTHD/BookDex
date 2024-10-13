import React from "react";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link } from "react-router-dom";
import { LinkContainer } from "react-router-bootstrap";
import { useAuth } from "../Context/useAuth";
import Button from "react-bootstrap/Button";

function NavigationBar() {
  const { isLoggedIn, user, logout } = useAuth();

  return (
    <Navbar bg="light" expand="lg">
      <Container>
        <LinkContainer to="/">
          <Navbar.Brand>BookDex</Navbar.Brand>
        </LinkContainer>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <LinkContainer to="/">
              <Nav.Link>Home</Nav.Link>
            </LinkContainer>
            {isLoggedIn() ? (
              <>
                <LinkContainer to="/cart">
                  <Nav.Link>Cart</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/library">
                  <Nav.Link>Library</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/order">
                  <Nav.Link>Orders</Nav.Link>
                </LinkContainer>
              </>
            ) : (
              <></>
            )}
          </Nav>
          <Nav>
            {isLoggedIn() ? (
              <>
                <LinkContainer to="/">
                  <Nav.Link>{user?.name}</Nav.Link>
                </LinkContainer>
                <Button
                  variant="danger"
                  size="sm"
                  className="pb-1.5 mt-1"
                  onClick={logout}
                >
                  Logout
                </Button>
              </>
            ) : (
              <>
                <LinkContainer to="/login">
                  <Nav.Link>Login</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/register">
                  <Nav.Link>Register</Nav.Link>
                </LinkContainer>
              </>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default NavigationBar;
