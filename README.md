# 🚗 Sistema de Administración de Estacionamientos 🅿️

¡Bienvenido al **Sistema de Administración de Estacionamientos**! Este sistema permite gestionar el ingreso, salida y administración de vehículos en un estacionamiento de manera eficiente.

---

## 📌 **Flujo del Sistema**
### 🚙 **Ingreso al Estacionamiento**
1. El conductor elige el **tipo de vehículo**:
   - 🏍️ **Motocicleta**
   - 🚗 **Carro / Jeepeta**
   - 🚛 **Camión**
2. Pulsa el botón **"Ingresar"**.
3. El sistema genera un **ticket único (código)** y registra:
   - 📆 **Fecha y hora de ingreso**.
   - 🚘 **Tipo de vehículo**.
   - 🔢 **Código generado**.

### 🚦 **Salida del Estacionamiento**
1. El conductor **ingresa el código del ticket**.
2. El sistema **calcula el costo a pagar** según las reglas de negocio.
3. Se muestra el **total a pagar** y se registra la salida.

---

## 📌 **Reglas de Negocio**
### 💰 **Tarifa y Pago**
- Se establece una **tarifa por hora o fracción**, dependiendo del **tipo de vehículo**.
- 🕒 **Los primeros 15 minutos son gratuitos**.
- A partir del minuto **16**, se cobra **una hora completa**.
  
#### **Ejemplos de cobro**:
| **Tiempo Estacionado** | **Pago** |
|-----------------|----------------|
| 15 minutos o menos | ✅ **Gratis** |
| 16 minutos o más | 💲 **1 hora** |
| 1 hora y 15 minutos | 💲 **1 hora** |
| 1 hora y 16 minutos | 💲 **2 horas** |
| Y así sucesivamente... | 🔄 |

---

## 📌 **Ocupación y Disponibilidad**
El sistema administra la **cantidad de estacionamientos disponibles** para cada tipo de vehículo.

🔹 **Si todos los espacios están ocupados**, el sistema mostrará un mensaje:  
🚫 **"No hay disponibilidad"** y no permitirá el ingreso.

---

## 📌 **Administración del Sistema**
👨‍💻 **Vista del Administrador** (requiere login):

✅ **Funcionalidades:**
- 🔧 **Configurar tarifas** por tipo de vehículo.
- 🅿️ **Administrar la cantidad de estacionamientos** disponibles.
- 📊 **Ver ocupación en tiempo real**.
- 🔍 **Consultar detalles de cada estacionamiento ocupado**, incluyendo:
  - Fecha de ingreso.
  - Tiempo transcurrido.
  - Código del ticket.
  - Tipo de vehículo.

---

## 🚀 **Pruebas del Proyecto**
Para validar el funcionamiento, el sistema debe contar con **registros de fechas diversas** que permitan comprobar:
- ⏳ Cálculo correcto del tiempo y tarifa.
- 🅿️ Control adecuado de la disponibilidad del estacionamiento.
- 📜 Registro de ingresos y salidas de vehículos.

---

## ⚡ **Tecnologías Utilizadas**
✅ **C#** (ASP.NET Core)  
✅ **Entity Framework** (para manejo de datos)  
✅ **SQL Server** (base de datos)  
✅ **MVC** (arquitectura del proyecto)  

---

