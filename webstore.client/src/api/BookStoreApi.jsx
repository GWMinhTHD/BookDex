import axios from "axios";
import { handleError } from "../Helper/ErrorHandler";
import { toast } from "react-toastify";


const axiosClient = axios.create({
  baseURL: "https://localhost:7216/",
});

const getAll = async (content) => {
  return await axiosClient.get(`api/${content}`);
};

const getById = async (content, id) => {
  return await axiosClient.get(`api/${content}/${id}`);
};

const login = async (email, password) => {
  try {
    const data = axiosClient.post(`api/auth/login`, {
      email: email,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

const register = async (email, userName, password) => {
  try {
    const data = axiosClient.post(`api/auth/register`, {
      email: email,
      userName: userName,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

const addCart = async (id) => {
  const token = localStorage.getItem("token");
  try {
    await axiosClient.post(`api/Cart/${id}`, null, {
      headers: { Authorization: `Bearer ${token}` },
    }).then((res) => {toast.success("Book added to Cart")});
  } catch (error) {
    handleError(error);
  }
}

const removeCart = async (id) => {
  const token = localStorage.getItem("token");
  return await axiosClient.delete(`api/Cart/${id}`, {
    headers: { Authorization: `Bearer ${token}` },
  })
}

const getUserCart = async () => {
  const token  = localStorage.getItem("token");
  return await axiosClient.get(`api/Cart`, {
    headers: { Authorization: `Bearer ${token}` },
  });
}

const BookStoreApi = {
  getAll,
  getById,
  login,
  register,
  addCart,
  removeCart,
  getUserCart
};

export default BookStoreApi;
