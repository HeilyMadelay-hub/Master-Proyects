Guía de Estructura para API Node.js + TypeScript

Creamos la carpeta 

mkdir event-ticketing-api
cd event-ticketing-api

Mergeamos el gitignore de la rama principal 

git fetch origin
git checkout origin/main -- .gitignore

Hacemos el commit 

git add .gitignore
git commit -m "Merge .gitignore from main"

Subimos a la rama

git push origin event-ticketing-api

Inicializar npm

npm init -y

Instalar TypeScript y configuración base

npm install -D typescript @types/node ts-node nodemon
npm install -D eslint prettier eslint-config-prettier
npm install -D @typescript-eslint/parser @typescript-eslint/eslint-plugin

Crear tsconfig.json

npx tsc --init

Y rellenarlo con 

{
  "compilerOptions": {
    // Versión de JavaScript que generará TypeScript
    "target": "ES2020", 

    // Sistema de módulos que se usará en el JS compilado
    "module": "commonjs",

    // Librerías de tipos disponibles (APIs de ES2020)
    "lib": ["ES2020"],

    // Carpeta donde se guardará el JS compilado
    "outDir": "./dist",

    // Carpeta donde está tu código fuente
    "rootDir": "./src",

    // Activa todas las comprobaciones estrictas de TypeScript
    "strict": true,

    // Permite importar módulos CommonJS con sintaxis moderna
    "esModuleInterop": true,

    // Omite verificación de tipos en node_modules (acelera compilación)
    "skipLibCheck": true,

    // Obliga a usar mayúsculas/minúsculas consistentes en imports
    "forceConsistentCasingInFileNames": true,

    // Permite importar archivos JSON directamente
    "resolveJsonModule": true,

    // Estrategia para resolver módulos (igual que Node.js)
    "moduleResolution": "node",

    // Error si hay variables declaradas y no usadas
    "noUnusedLocals": true,

    // Error si hay parámetros que no se usan
    "noUnusedParameters": true,

    // Todas las rutas de una función deben devolver algo
    "noImplicitReturns": true,

    // Evita olvidos en switch (fallthrough)
    "noFallthroughCasesInSwitch": true
  },

  // Archivos que se incluirán en la compilación
  "include": ["src/**/*"],

  // Archivos/carpetas que se excluirán
  "exclude": ["node_modules", "dist"]
}

# 📚 Guía Completa: Estructura para API Node.js + TypeScript

## 🎯 Estructura Base Recomendada

```
project-name/
│
├── src/
│   ├── config/          # Configuración (env, DB, etc.)
│   ├── db/              # Cliente de base de datos (opcional)
│   ├── models/          # Modelos de datos (ORM)
│   ├── repositories/    # Acceso a datos
│   ├── services/        # Lógica de negocio
│   ├── controllers/     # Manejo de peticiones HTTP
│   ├── routes/          # Definición de endpoints
│   ├── middlewares/     # Auth, validación, CORS, etc.
│   ├── utils/           # Funciones reutilizables
│   ├── validators/      # Validaciones de datos
│   ├── errors/          # Manejo centralizado de errores
│   ├── types/           # Interfaces y tipos TypeScript
│   ├── constants/       # Constantes (roles, estados, etc.)
│   ├── jobs/            # Background jobs (opcional)
│   ├── websocket/       # WebSockets (opcional)
│   ├── app.ts           # Configuración de Express
│   └── main.ts          # Punto de entrada del servidor
│
├── tests/               # Tests unitarios e integración
├── dist/                # Código compilado (JS)
├── node_modules/
├── .env
├── .gitignore
├── package.json
├── tsconfig.json
└── README.md
```

---

## 📦 Responsabilidad de cada carpeta

### 🔴 **CARPETAS OBLIGATORIAS** (toda API debe tenerlas)

| Carpeta | Propósito | Ejemplo | ¿Por qué es obligatoria? |
|---------|-----------|---------|--------------------------|
| **models/** | Definición de entidades (Prisma, Sequelize, Mongoose) | `User.model.ts` | Sin modelos no hay estructura de datos |
| **services/** | Lógica de negocio compleja | `AuthService.ts` | Separar lógica de controllers |
| **controllers/** | Recibe request, devuelve response (sin lógica) | `UserController.ts` | Punto de entrada HTTP |
| **routes/** | Define endpoints (`/users`, `/auth`) | `user.routes.ts` | Necesario para Express |
| **middlewares/** | Funciones intermedias (auth, logs, CORS) | `authMiddleware.ts` | Auth, validación, errores |
| **config/** | Variables de entorno, config DB | `database.config.ts` | Centralizar configuración |
| **utils/** | Helpers generales (JWT, bcrypt, logger) | `jwt.util.ts` | Funciones reutilizables |
| **types/** | Interfaces y tipos TS | `express.d.ts`, `enums.ts` | TypeScript necesita tipos |
| **errors/** | Clases de error personalizadas | `AppError.ts` | Manejo centralizado de errores |

### 🟡 **CARPETAS MUY RECOMENDADAS** (según el proyecto)

| Carpeta | Cuándo usarla | Ejemplo | Tipo de proyecto |
|---------|---------------|---------|------------------|
| **repositories/** | Cuando tienes consultas complejas a BD | `UserRepository.ts` | APIs medianas/grandes, operaciones atómicas |
| **validators/** | Validaciones con Zod/Joi | `userValidator.ts` | Todas las APIs (mejor que validar en controllers) |
| **constants/** | Evitar valores mágicos | `roles.ts`, `orderTimeout.ts` | Cuando tienes muchos valores fijos |
| **jobs/** o **queues/** | Background tasks | `orderExpirationJob.ts` | APIs con tareas asíncronas (emails, limpieza) |
| **websocket/** | Comunicación en tiempo real | `handlers.ts`, `rooms.ts` | APIs con notificaciones en vivo |
| **db/** | Conexión separada a BD | `connection.ts` | Si manejas múltiples conexiones |

### 🟢 **CARPETAS OPCIONALES** (casos específicos)

| Carpeta | Cuándo usarla | Ejemplo |
|---------|---------------|---------|
| **decorators/** | Solo con clases + metadata | `@Role('admin')` |
| **tests/** | Siempre recomendable | `user.test.ts` |
| **docs/** | Documentación API | `swagger.yaml` |
| **scripts/** | Automatizaciones | `seed.ts`, `migrate.ts` |

---

## 📊 Matriz de decisión: ¿Qué carpetas necesito?

### **API Pequeña** (CRUD básico, < 10 endpoints)
```
✅ models, services, controllers, routes, middlewares
✅ config, utils, types, errors
❌ repositories, validators, constants
❌ jobs, websocket, decorators
```

### **API Mediana** (Sistema completo, 10-30 endpoints)
```
✅ TODO lo anterior +
✅ repositories, validators, constants
🟡 jobs (si tienes tareas programadas)
❌ websocket, decorators
```

### **API Compleja** (Tiempo real, concurrencia, background jobs)
```
✅ TODO lo anterior +
✅ jobs, websocket, tests
✅ db (si múltiples conexiones)
🟡 decorators (solo si usas clases)
```

---

## 🔄 Flujo de una petición (arquitectura)

```
Request HTTP
  ↓
Routes (define endpoint)
  ↓
Middlewares (auth, validación)
  ↓
Validators (Zod/Joi schemas)
  ↓
Controllers (recibe y delega)
  ↓
Services (lógica de negocio)
  ↓
Repositories (acceso a BD)
  ↓
Models (ORM)
  ↓
Database
```

**Reglas de flujo:**
- **Controllers** → Solo delegan, no tienen lógica
- **Services** → Toda la lógica de negocio
- **Repositories** → Solo queries a BD (sin lógica)
- **Models** → Solo estructura de datos

---

## 🎯 Ejemplos prácticos por tipo de proyecto

### **Ejemplo 1: API de Autenticación simple**
```
src/
├── config/          # DB + JWT config
├── models/          # User.ts
├── services/        # authService.ts
├── controllers/     # authController.ts
├── routes/          # authRoutes.ts
├── middlewares/     # authMiddleware.ts
├── utils/           # jwt.ts, bcrypt.ts
├── types/           # user.interface.ts
├── errors/          # AppError.ts
├── app.ts
└── main.ts
```
**NO necesitas:** repositories, validators, constants, jobs, websocket

---

### **Ejemplo 2: E-commerce con inventario**
```
src/
├── config/
├── models/          # Product, Order, User
├── repositories/    # productRepository.ts (stock atómico)
├── services/        # orderService.ts, inventoryService.ts
├── controllers/
├── routes/
├── middlewares/
├── validators/      # orderValidator.ts (Zod)
├── utils/
├── types/           # enums.ts (OrderStatus)
├── constants/       # orderStates.ts
├── errors/
├── app.ts
└── main.ts
```
**Añades:** repositories (operaciones atómicas), validators, constants

---

### **Ejemplo 3: Sistema de tickets con tiempo real** (tu caso)
```
src/
├── config/          # DB, JWT, WebSocket
├── models/          # Event, Order, Ticket, User
├── repositories/    # orderRepository.ts ($inc atómico)
├── services/        # orderService.ts, ticketService.ts
├── controllers/
├── routes/
├── middlewares/
├── validators/      # orderValidator.ts
├── jobs/            # orderExpirationJob.ts
├── websocket/       # handlers.ts, rooms.ts
├── utils/
├── types/           # enums.ts (OrderStatus, EventStatus)
├── constants/       # orderTimeout.ts, roles.ts
├── errors/
├── app.ts
└── main.ts
```
**Añades:** repositories, validators, constants, jobs, websocket

---

## ⚙️ Configuración TypeScript obligatoria

**tsconfig.json**
```json
{
  "compilerOptions": {
    "target": "ES2020",
    "module": "commonjs",
    "outDir": "./dist",
    "rootDir": "./src",
    "strict": true,
    "esModuleInterop": true,
    "skipLibCheck": true,
    "experimentalDecorators": true,  // Solo si usas decorators
    "emitDecoratorMetadata": false   // false por defecto
  },
  "include": ["src/**/*"],
  "exclude": ["node_modules", "dist"]
}
```

---

## 🔴 Elementos ESENCIALES (implementación)

### 1️⃣ Manejo de errores centralizado
```typescript
// errors/AppError.ts
export class AppError extends Error {
  constructor(
    public message: string, 
    public statusCode: number = 500,
    public code?: string
  ) {
    super(message);
    this.name = 'AppError';
  }
}

// errors/errorCodes.ts
export const ErrorCodes = {
  VALIDATION_ERROR: 'VALIDATION_ERROR',
  UNAUTHORIZED: 'UNAUTHORIZED',
  NOT_FOUND: 'NOT_FOUND',
  INSUFFICIENT_STOCK: 'INSUFFICIENT_STOCK'
} as const;
```

### 2️⃣ Utilidades JWT
```typescript
// utils/jwt.ts
import jwt from 'jsonwebtoken';

export const generateToken = (payload: object): string => {
  return jwt.sign(payload, process.env.JWT_SECRET!, {
    expiresIn: process.env.JWT_EXPIRES_IN || '7d'
  });
};

export const verifyToken = (token: string) => {
  return jwt.verify(token, process.env.JWT_SECRET!);
};
```

### 3️⃣ Tipos personalizados
```typescript
// types/express.d.ts
import { User } from '../models/User';

declare global {
  namespace Express {
    interface Request {
      user?: User;
    }
  }
}

// types/enums.ts
export enum OrderStatus {
  PENDING = 'PENDING',
  RESERVED = 'RESERVED',
  CONFIRMED = 'CONFIRMED',
  CANCELLED = 'CANCELLED',
  EXPIRED = 'EXPIRED'
}
```

---

## 🟡 Decorators vs Middlewares: ¿Cuándo usar cada uno?

### **Usa Middlewares cuando:**
✅ Lógica a nivel de ruta (auth, CORS, body-parser)  
✅ API funcional (sin clases)  
✅ Express estándar

```typescript
// middlewares/authMiddleware.ts
export const authenticate = (req, res, next) => {
  const token = req.headers.authorization?.split(' ')[1];
  if (!token) throw new AppError('Unauthorized', 401);
  
  req.user = verifyToken(token);
  next();
};
```

### **Usa Decorators cuando:**
✅ Trabajas con **clases** (controllers como clases)  
✅ Usas frameworks como **NestJS** o **TypeORM**  
✅ Necesitas metadata avanzada (roles, permisos)

```typescript
// decorators/role.decorator.ts
export function RequireRole(role: string) {
  return function (target: any, propertyKey: string, descriptor: PropertyDescriptor) {
    const originalMethod = descriptor.value;
    descriptor.value = async function (...args: any[]) {
      const req = args[0];
      if (req.user.role !== role) {
        throw new AppError('Forbidden', 403);
      }
      return originalMethod.apply(this, args);
    };
  };
}

// Uso:
class UserController {
  @RequireRole('ADMIN')
  async deleteUser(req, res) { ... }
}
```

**Tabla comparativa:**

| Concepto | Uso | Sintaxis | Complejidad |
|----------|-----|----------|-------------|
| **Middlewares** | Lógica a nivel de ruta | Funciones | Simple |
| **Decorators** | Metadata a nivel de clase/método | `@Decorator` | Avanzada |

**Recomendación:** Empieza con **middlewares**. Solo usa decorators si ya trabajas con clases.

---

## 🚀 Scripts de PowerShell para crear estructura

### **Script 1: Estructura básica (API pequeña/mediana)**
```powershell
# Carpetas principales
"config", "models", "services", "controllers", "routes", 
"middlewares", "utils", "types", "errors" | 
ForEach-Object { New-Item -ItemType Directory -Path "src\$_" -Force }

# Archivos principales
New-Item -ItemType File -Path "src\app.ts" -Force
New-Item -ItemType File -Path "src\main.ts" -Force

# Archivos de configuración
New-Item -ItemType File -Path ".env.example" -Force
New-Item -ItemType File -Path ".gitignore" -Force
New-Item -ItemType File -Path "tsconfig.json" -Force
```

### **Script 2: Estructura completa (API compleja)**
```powershell
# Carpetas base
"config", "models", "repositories", "services", "controllers", 
"routes", "middlewares", "validators", "utils", "types", 
"errors", "constants" | 
ForEach-Object { New-Item -ItemType Directory -Path "src\$_" -Force }

# Carpetas avanzadas (tiempo real, jobs)
"jobs", "websocket" | 
ForEach-Object { New-Item -ItemType Directory -Path "src\$_" -Force }

# Tests
"unit", "integration" | 
ForEach-Object { New-Item -ItemType Directory -Path "tests\$_" -Force }

# Archivos principales
New-Item -ItemType File -Path "src\app.ts" -Force
New-Item -ItemType File -Path "src\main.ts" -Force

# Configuración
New-Item -ItemType File -Path ".env.example" -Force
New-Item -ItemType File -Path ".gitignore" -Force
New-Item -ItemType File -Path "tsconfig.json" -Force
New-Item -ItemType File -Path "README.md" -Force
```

---

## 💡 Reglas de oro

1. **Empieza simple** → Agrega carpetas solo cuando las necesites
2. **Controllers sin lógica** → Delegan todo a services
3. **Services sin SQL** → Usan repositories
4. **Un archivo, una responsabilidad**
5. **Constantes > strings mágicos**
6. **Tipos > any**
7. **Errores centralizados** → No uses `throw new Error()` directamente

---

## 🎯 Checklist: ¿Qué carpetas necesito?

**Pregúntate esto:**

- ✅ **¿Tengo operaciones atómicas en BD?** → Añade `repositories/`
- ✅ **¿Uso Zod/Joi para validar?** → Añade `validators/`
- ✅ **¿Tengo muchos valores fijos?** → Añade `constants/`
- ✅ **¿Necesito tareas programadas?** → Añade `jobs/`
- ✅ **¿Uso WebSockets?** → Añade `websocket/`
- ✅ **¿Trabajo con clases + metadata?** → Añade `decorators/`
- ✅ **¿Múltiples conexiones a BD?** → Añade `db/`

---

## 📌 Resumen visual rápido

```
🔴 OBLIGATORIO (toda API):
   models, services, controllers, routes, middlewares
   config, utils, types, errors

🟡 MUY RECOMENDADO (según proyecto):
   repositories, validators, constants
   jobs, websocket

🟢 OPCIONAL (casos específicos):
   decorators (solo con clases), tests, docs, db
```

---

## 🚦 Siguiente paso

1. **Identifica el tipo de tu proyecto** (pequeña/mediana/compleja)
2. **Crea la estructura base** con el script correspondiente
3. **Define tus enums y tipos** en `types/`
4. **Implementa modelos** en `models/`
5. **Empieza por un servicio simple** en `services/`

¿Dónde meter el Docker en una API pequeña/mediana/compleja?