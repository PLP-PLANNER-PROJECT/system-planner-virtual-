const calendar = document.getElementById('calendar');
const monthName = document.getElementById('month-name');
const daysContainer = document.getElementById('days');
const prev = document.getElementById('prev');
const next = document.getElementById('next');

let currentDate = new Date();

const months = [
    'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
    'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'
];

function renderCalendar(date) {
    monthName.innerText = `${months[date.getMonth()]} ${date.getFullYear()}`;

    const firstDayOfMonth = new Date(date.getFullYear(), date.getMonth(), 1).getDay();
    const daysInMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();

    daysContainer.innerHTML = '';

    // Preencher os dias em branco antes do 1º dia do mês
    for (let i = 0; i < firstDayOfMonth; i++) {
        daysContainer.innerHTML += '<div></div>';
    }

    // Preencher os dias do mês
    for (let i = 1; i <= daysInMonth; i++) {
        daysContainer.innerHTML += `<div>${i}</div>`;
    }
}

prev.addEventListener('click', () => {
    currentDate.setMonth(currentDate.getMonth() - 1);
    renderCalendar(currentDate);
});

next.addEventListener('click', () => {
    currentDate.setMonth(currentDate.getMonth() + 1);
    renderCalendar(currentDate);
});


renderCalendar(currentDate);

function redirectToTaskPage() {
    window.location.href = "/Tarefa/Adicionar"; 
}