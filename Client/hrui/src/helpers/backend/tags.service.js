import { authHeader } from "./auth-header";

export const tagService = {
  getTags,
  getTag,
  createTag,
  editTag,
};
// create new tag
const createTag = (data) => {
  const req = {
    method: "POST",
    Headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  };
  return fetch(`/tags`, req)
    .then((res) => res.json())
    .then((data) => {})
    .catch((err) => {});
};
// edit tag
const editTag = (data) => {
  const req = {
    method: "PUT",
    Headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  };
  return fetch(`/tags/${id}`, req)
    .then((res) => res.json())
    .then((data) => {})
    .catch((err) => {});
};
//fetch all tags
const getTags = () => {
  const req = {
    method: "GET",
    Headers: { "Content-Type": "application/json" },
  };
  return fetch(`/tags`, req)
    .then((res) => res.json())
    .then((data) => {})
    .catch((err) => {});
};
// fetch tag by id
const getTag = (id) => {
  const req = {
    method: "GET",
    Headers: { "Content-Type": "application/json" },
  };
  return fetch(`/tags/getbyid/${id}`, req)
    .then((res) => res.json())
    .then((data) => {})
    .catch((err) => {});
};
