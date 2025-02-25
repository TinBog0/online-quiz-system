﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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
