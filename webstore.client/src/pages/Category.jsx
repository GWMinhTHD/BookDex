import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/Table";
import BookStoreApi from "../api/BookStoreApi";

function Category() {
  const [categories, setCategories] = useState({});
  useEffect(() => {
    BookStoreApi.getAll("Category").then((item) => setCategories(item.data));
  }, []);

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>Id</th>
          <th>Genre</th>
          <th>Description</th>
        </tr>
      </thead>
      <tbody>
        {categories.length > 0 &&
          categories.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.name}</td>
              <td>{item.description}</td>
            </tr>
          ))}
      </tbody>
    </Table>
  );
}

export default Category;
