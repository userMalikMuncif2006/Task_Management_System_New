async function loadDashboard() {
    const res = await apiRequest("Task");
    const tasks = res.data;

    document.getElementById('countTotal').innerText = tasks.length;
    document.getElementById('countTodo').innerText = tasks.filter(t => t.status === 'Todo').length;
    document.getElementById('countProgress').innerText = tasks.filter(t => t.status === 'In Progress').length;
    document.getElementById('countDone').innerText = tasks.filter(t => t.status === 'Done').length;

    const tbody = document.getElementById('recentTasks');
    tasks.slice(-5).forEach(t => {
        tbody.innerHTML += `<tr><td>${t.title}</td><td>${t.userName}</td><td>${t.status}</td></tr>`;
    });
}
loadDashboard();