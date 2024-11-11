import React, { useState, useEffect } from "react";
import { useAuth } from "../../Context/useAuth";

const ProfilePage = () => {
  const { updateName, changePassword } = useAuth();
  const [name, setName] = useState("");
  const [userPassword, setUserPassword] = useState("");
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [nameError, setNameError] = useState("");
  const [passwordError, setPasswordError] = useState("");

  useEffect(() => {
    const storedUser = JSON.parse(localStorage.getItem("user"));
    if (storedUser) {
      setName(storedUser.name);
    }
  }, []);

  const validatePassword = (password) => {
    const minLength = 8;
    const hasNumber = /\d/.test(password);
    const hasLowercase = /[a-z]/.test(password);

    return password.length >= minLength && hasNumber && hasLowercase;
  };

  const handleNameSubmit = async (e) => {
    e.preventDefault();
    setNameError("");

    if (name.trim() === "") {
      setNameError("Name cannot be empty.");
      return;
    }

    if (userPassword.trim() === "") {
      setNameError("Password is required to update username.");
      return;
    }

    updateName(name, userPassword);
  };

  const handlePasswordSubmit = async (e) => {
    e.preventDefault();
    setPasswordError("");

    if (!validatePassword(newPassword)) {
      setPasswordError(
        "New password must be at least 8 characters long and contain at least 1 number and 1 lowercase letter."
      );
      return;
    }

    changePassword(oldPassword, newPassword);
  };

  return (
   <div className="mx-auto p-4 bg-gray-900 min-h-screen">
      <div className="max-w-2xl mx-auto">
        <h1 className="text-3xl font-bold text-gray-100 mb-6">
          Profile Settings
        </h1>

        {/* Username Update Form */}
        <div className="bg-gray-800 shadow-md rounded-lg overflow-hidden mb-6">
          <div className="px-6 py-4">
            <h2 className="text-xl font-bold text-gray-100 mb-4">
              Update Username
            </h2>
            <form onSubmit={handleNameSubmit} className="space-y-4">
              <div>
                <label
                  htmlFor="username"
                  className="block text-sm font-medium text-gray-300 mb-1"
                >
                  Username
                </label>
                <input
                  id="username"
                  type="text"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  required
                  className="w-full px-3 py-2 bg-gray-700 text-gray-100 border border-gray-600 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
              </div>
              <div>
                <label
                  htmlFor="usernamePassword"
                  className="block text-sm font-medium text-gray-300 mb-1"
                >
                  Password
                </label>
                <input
                  id="usernamePassword"
                  type="password"
                  value={userPassword}
                  onChange={(e) => setUserPassword(e.target.value)}
                  required
                  className="w-full px-3 py-2 bg-gray-700 text-gray-100 border border-gray-600 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
              </div>
              {nameError && (
                <div
                  className="bg-red-900 border-l-4 border-red-500 text-red-100 p-4"
                  role="alert"
                >
                  <p className="font-bold">Error</p>
                  <p>{nameError}</p>
                </div>
              )}
              <button
                type="submit"
                className="w-full bg-blue-600 text-white font-semibold py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
              >
                Update Username
              </button>
            </form>
          </div>
        </div>

        {/* Password Change Form */}
        <div className="bg-gray-800 shadow-md rounded-lg overflow-hidden">
          <div className="px-6 py-4">
            <h2 className="text-xl font-bold text-gray-100 mb-4">
              Change Password
            </h2>
            <form onSubmit={handlePasswordSubmit} className="space-y-4">
              <div>
                <label
                  htmlFor="oldPassword"
                  className="block text-sm font-medium text-gray-300 mb-1"
                >
                  Old Password
                </label>
                <input
                  id="oldPassword"
                  type="password"
                  value={oldPassword}
                  onChange={(e) => setOldPassword(e.target.value)}
                  required
                  className="w-full px-3 py-2 bg-gray-700 text-gray-100 border border-gray-600 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
              </div>
              <div>
                <label
                  htmlFor="newPassword"
                  className="block text-sm font-medium text-gray-300 mb-1"
                >
                  New Password
                </label>
                <input
                  id="newPassword"
                  type="password"
                  value={newPassword}
                  onChange={(e) => setNewPassword(e.target.value)}
                  required
                  className="w-full px-3 py-2 bg-gray-700 text-gray-100 border border-gray-600 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                />
              </div>
              {passwordError && (
                <div
                  className="bg-red-900 border-l-4 border-red-500 text-red-100 p-4"
                  role="alert"
                >
                  <p className="font-bold">Error</p>
                  <p>{passwordError}</p>
                </div>
              )}
              <button
                type="submit"
                className="w-full bg-blue-600 text-white font-semibold py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
              >
                Change Password
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;
