import "";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/Table";
import BookStoreApi from "../../api/BookStoreApi";

//import Books from "../components/Book/Books";

function HomePage() {
  const [books, setBooks] = useState({});
  useEffect(() => {
    BookStoreApi.getAll("Book").then((item) => setBooks(item.data));
  }, []);

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>Id</th>
          <th>Name</th>
          <th>Cover</th>
          <th>Description</th>
          <th>Price</th>
          <th>Author</th>
          <th>Genre</th>
        </tr>
      </thead>
      <tbody>
        {books.length > 0 &&
          books.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.name}</td>
              <td>{item.cover}</td>
              <td>{item.description}</td>
              <td>{item.price}</td>
              <td>{item.authors.join(", ")}</td>
              <td>{item.categories.join(", ")}</td>
              {/*<td>
                {item["authors"].map((a) => (
                  <span key={a}>{a} </span>
                ))}
              </td>
              <td>
                {item["categories"].map((c) => (
                  <span key={c}>{c} </span>
                ))}
              </td>*/}
            </tr>
          ))}
      </tbody>
    </Table>
  );
}

export default HomePage;
