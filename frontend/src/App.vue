<script setup>
import { onMounted, ref } from 'vue'

const API_URL = 'http://localhost:5050/api/tasks'

const title = ref('')
const tasks = ref([])
const loading = ref(false)
const saving = ref(false)
const errorMessage = ref('')

async function loadTasks() {
  loading.value = true
  errorMessage.value = ''

  try {
    const response = await fetch(API_URL)

    if (!response.ok) {
      throw new Error('A API não conseguiu retornar as tarefas.')
    }

    tasks.value = await response.json()
  } catch (error) {
    errorMessage.value = error.message
  } finally {
    loading.value = false
  }
}

async function addTask() {
  const trimmedTitle = title.value.trim()

  if (!trimmedTitle) {
    errorMessage.value = 'Digite o título da tarefa.'
    return
  }

  saving.value = true
  errorMessage.value = ''

  try {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        title: trimmedTitle,
        completed: false,
      }),
    })

    if (!response.ok) {
      throw new Error('A tarefa não pôde ser cadastrada.')
    }

    title.value = ''
    await loadTasks()
  } catch (error) {
    errorMessage.value = error.message
  } finally {
    saving.value = false
  }
}

onMounted(loadTasks)
</script>

<template>
  <main class="page">
    <section class="panel">
      <p class="brand">TrivalApps Microlearning</p>

      <h1>Lista de tarefas</h1>

      <p class="description">
        Vue.js no frontend, .NET no backend e MongoDB no banco de dados.
      </p>

      <form class="task-form" @submit.prevent="addTask">
        <label for="title">Nova tarefa</label>

        <div class="form-row">
          <input
            id="title"
            v-model="title"
            type="text"
            placeholder="Ex.: Revisar a etapa sobre MongoDB"
            autocomplete="off"
          />

          <button type="submit" :disabled="saving">
            {{ saving ? 'Salvando...' : 'Adicionar' }}
          </button>
        </div>
      </form>

      <p v-if="errorMessage" class="error-message">
        {{ errorMessage }}
      </p>

      <p v-if="loading" class="status-message">
        Carregando tarefas...
      </p>

      <ul v-else-if="tasks.length" class="task-list">
        <li v-for="task in tasks" :key="task.id">
          <span>{{ task.title }}</span>

          <small>
            {{ task.completed ? 'Concluída' : 'Pendente' }}
          </small>
        </li>
      </ul>

      <p v-else class="status-message">
        Nenhuma tarefa cadastrada.
      </p>
    </section>
  </main>
</template>

<style scoped>
:global(*) {
  box-sizing: border-box;
}

:global(body) {
  margin: 0;
  background: #f4f6f8;
  color: #172033;
  font-family: Arial, sans-serif;
}

.page {
  min-height: 100vh;
  padding: 48px 20px;
}

.panel {
  width: min(760px, 100%);
  margin: 0 auto;
  padding: 32px;
  border: 1px solid #d8dee8;
  border-radius: 16px;
  background: #ffffff;
  box-shadow: 0 14px 36px rgba(23, 32, 51, 0.08);
}

.brand {
  margin: 0 0 8px;
  color: #2d6cdf;
  font-size: 0.85rem;
  font-weight: 700;
  letter-spacing: 0.06em;
  text-transform: uppercase;
}

h1 {
  margin: 0;
  font-size: 2rem;
}

.description {
  margin: 10px 0 28px;
  color: #5f6b7a;
}

.task-form label {
  display: block;
  margin-bottom: 8px;
  font-weight: 700;
}

.form-row {
  display: flex;
  gap: 10px;
}

input {
  flex: 1;
  min-width: 0;
  padding: 12px 14px;
  border: 1px solid #b8c2d1;
  border-radius: 8px;
  font: inherit;
}

input:focus {
  border-color: #2d6cdf;
  outline: 3px solid rgba(45, 108, 223, 0.16);
}

button {
  padding: 12px 18px;
  border: 0;
  border-radius: 8px;
  background: #2d6cdf;
  color: #ffffff;
  font: inherit;
  font-weight: 700;
  cursor: pointer;
}

button:disabled {
  cursor: wait;
  opacity: 0.65;
}

.error-message {
  margin: 18px 0 0;
  padding: 12px;
  border-radius: 8px;
  background: #fff0f0;
  color: #b42318;
}

.status-message {
  margin: 24px 0 0;
  color: #667085;
}

.task-list {
  display: grid;
  gap: 10px;
  margin: 28px 0 0;
  padding: 0;
  list-style: none;
}

.task-list li {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 16px;
  padding: 14px 16px;
  border: 1px solid #e0e5ec;
  border-radius: 10px;
}

.task-list small {
  color: #667085;
}

@media (max-width: 600px) {
  .panel {
    padding: 24px;
  }

  .form-row {
    flex-direction: column;
  }
}
</style>