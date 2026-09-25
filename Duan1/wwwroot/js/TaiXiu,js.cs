document.addEventListener("DOMContentLoaded", () =>
{
    // DOM Elements
    const taiXiuModal = document.getElementById("taiXiuModal");
    const openTaiXiuBtn = document.getElementById("openTaiXiuBtn");
    const closeTaiXiuBtn = document.getElementById("closeTaiXiuBtn");
    const countdownTimer = document.getElementById("countdownTimer");
    const diceContainer = document.getElementById("diceContainer");
    const winResultTag = document.getElementById("winResultTag");
    const historyTrail = document.getElementById("historyTrail");
    const userBalanceEl = document.getElementById("userBalance");

    const chooseTai = document.getElementById("chooseTai");
    const chooseXiu = document.getElementById("chooseXiu");
    const myBetTaiEl = document.getElementById("myBetTai");
    const myBetXiuEl = document.getElementById("myBetXiu");

    const chips = document.querySelectorAll(".chip");
    const btnConfirmBet = document.getElementById("btnConfirmBet");
    const btnCancelBet = document.getElementById("btnCancelBet");
    const btnAllIn = document.getElementById("btnAllIn");

    // Game States
    let userBalance = 5028480;
    let selectedSide = null; // 'TAI' hoặc 'XIU'
    let selectedChipValue = 100000;
    let myBets = { TAI: 0, XIU: 0 };
let secondsLeft = 45;
let isRolling = false;

// Open/Close Modal
openTaiXiuBtn.addEventListener("click", () => taiXiuModal.classList.add("show"));
closeTaiXiuBtn.addEventListener("click", () => taiXiuModal.classList.remove("show"));

// Select Chip
chips.forEach(chip =>
{
    chip.addEventListener("click", () =>
    {
        chips.forEach(c => c.classList.remove("active"));
        chip.classList.add("active");
        selectedChipValue = parseInt(chip.getAttribute("data-value"));
    });
});

// Select Side (Tai / Xiu)
const setSide = (side) =>
{
    selectedSide = side;
    if (side === "TAI")
    {
        chooseTai.classList.add("selected");
        chooseXiu.classList.remove("selected");
    }
    else
    {
        chooseXiu.classList.add("selected");
        chooseTai.classList.remove("selected");
    }
};

chooseTai.addEventListener("click", () => setSide("TAI"));
chooseXiu.addEventListener("click", () => setSide("XIU"));

// Đặt Cược
btnConfirmBet.addEventListener("click", () =>
{
    if (!selectedSide)
    {
        alert("Vui lòng chọn cửa đặt (TÀI hoặc XỈU)!");
        return;
    }
    if (userBalance < selectedChipValue)
    {
        alert("Số dư không đủ!");
        return;
    }

    userBalance -= selectedChipValue;
    myBets[selectedSide] += selectedChipValue;
    updateUI();
});

// All In
btnAllIn.addEventListener("click", () =>
{
    if (!selectedSide)
    {
        alert("Vui lòng chọn cửa đặt trước khi ALL-IN!");
        return;
    }
    if (userBalance <= 0) return;

    myBets[selectedSide] += userBalance;
    userBalance = 0;
    updateUI();
});

// Hủy Cược
btnCancelBet.addEventListener("click", () =>
{
    userBalance += myBets.TAI + myBets.XIU;
    myBets.TAI = 0;
    myBets.XIU = 0;
    updateUI();
});

function updateUI()
{
    userBalanceEl.innerText = userBalance.toLocaleString("vi-VN");
    myBetTaiEl.innerText = myBets.TAI.toLocaleString("vi-VN");
    myBetXiuEl.innerText = myBets.XIU.toLocaleString("vi-VN");
}

// ================= VÒNG LẶP ĐẾM NGƯỢC & MỞ BÁT =================
setInterval(() =>
{
    if (isRolling) return;

    secondsLeft--;
    if (secondsLeft > 5)
    {
        countdownTimer.style.display = "block";
        diceContainer.style.display = "none";
        winResultTag.style.display = "none";
        countdownTimer.innerText = secondsLeft;
    }
    else if (secondsLeft <= 5 && secondsLeft > 0)
    {
        // Chuẩn bị mở bát (5 giây cuối)
        countdownTimer.innerText = secondsLeft;
    }
    else
    {
        // Mở kết quả
        resolveRound();
    }
}, 1000);

function resolveRound()
{
    isRolling = true;
    countdownTimer.style.display = "none";
    diceContainer.style.display = "flex";

    // Xúc xắc ngẫu nhiên (1-6)
    const d1 = Math.floor(Math.random() * 6) + 1;
    const d2 = Math.floor(Math.random() * 6) + 1;
    const d3 = Math.floor(Math.random() * 6) + 1;
    const total = d1 + d2 + d3;
    const resultSide = total >= 11 ? "TAI" : "XIU";

    // Render Dices text
    const dices = diceContainer.querySelectorAll(".dice");
    [d1, d2, d3].forEach((val, idx) =>
    {
        dices[idx].innerText = val;
        dices[idx].style.textAlign = "center";
        dices[idx].style.lineHeight = "32px";
        dices[idx].style.fontWeight = "bold";
        dices[idx].style.color = "#000";
    });

    // Xử lý Thắng / Thua
    if (myBets[resultSide] > 0)
    {
        const winAmount = myBets[resultSide] * 1.98; // Tỉ lệ 1 ăn 0.98
        userBalance += winAmount;
        winResultTag.innerText = `+${ winAmount.toLocaleString("vi-VN")}`;
        winResultTag.style.display = "block";
    }

    // Cập nhật cầu lịch sử (History Trail)
    const dot = document.createElement("span");
    dot.className = `dot ${ resultSide.toLowerCase()}`;
    dot.innerText = resultSide === "TAI" ? "T" : "X";
    historyTrail.appendChild(dot);
    if (historyTrail.children.length > 10)
    {
        historyTrail.removeChild(historyTrail.children[0]);
    }

    // Reset ván mới sau 5 giây hiển thị kết quả
    setTimeout(() =>
    {
        myBets.TAI = 0;
        myBets.XIU = 0;
        secondsLeft = 45;
        isRolling = false;
        updateUI();
    }, 5000);
}
});