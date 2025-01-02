// timer.js

let timeLeft = 30;
const timerText = document.getElementById("timer-text");

const intervalId = setInterval(() => {
    timeLeft--;
    timerText.innerText = timeLeft;

    if (timeLeft <= 0) {
        clearInterval(intervalId);
        alert("Time's up!");
    }
}, 1000);
