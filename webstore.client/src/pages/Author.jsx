import React, { useEffect, useState } from "react";
import BookStoreApi from "../api/BookStoreApi";
import Table from "react-bootstrap/Table";

function Author() {
  const [authors, setAuthors] = useState({});
  useEffect(() => {
    BookStoreApi.getAll("Author").then((item) => setAuthors(item.data));
  }, []);

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>Id</th>
          <th>Photo</th>
          <th>Name</th>
          <th>Info</th>
        </tr>
      </thead>
      <tbody>
        {authors.length > 0 &&
          authors.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.photo}</td>
              <td>{item.alias}</td>
              <td>{item.info}</td>
            </tr>
          ))}
      </tbody>
    </Table>
  );
}

export default Author;
