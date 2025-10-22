function pad(n) {
  return n.toString().padStart(2, "0");
}

function updateTime() {
  let now = new Date();
  let hour = pad(now.getHours());
  let minutes = pad(now.getMinutes());
  document.getElementById("clock").textContent = `${hour}:${minutes}`;
}
updateTime();
setInterval(updateTime, 1000);
