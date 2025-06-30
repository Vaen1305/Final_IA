/*
════════════════════════════════════════════════════════════════════════════════
                    GUÍA COMPLETA DEL NUEVO SISTEMA AI - HAMBRE/SUEÑO
════════════════════════════════════════════════════════════════════════════════

CAMBIOS PRINCIPALES:
- Sistema de ESTAMINA eliminado
- Nuevo sistema de HAMBRE y SUEÑO
- Comportamientos específicos por animal
- Velocidades dinámicas según estado

════════════════════════════════════════════════════════════════════════════════
                                    🦌 CIERVO
════════════════════════════════════════════════════════════════════════════════

COMPORTAMIENTOS:
✅ Si tiene HAMBRE → Buscar y comer hierba
✅ Si tiene SUEÑO → Dormir
✅ SIEMPRE evita Lobos y Jabalís (huye más rápido)
✅ Si no tiene hambre ni sueño → Deambular tranquilamente

ESTRUCTURA BEHAVIOR TREE CIERVO:
ROOT (Selector)
├── SECUENCIA: Huida Urgente (Prioridad 1)
│   ├── ActionViewLobo
│   └── ActionRunAway
├── SECUENCIA: Evasión (Prioridad 2)
│   ├── ActionViewJabali  
│   └── ActionAvoid
├── SECUENCIA: Dormir (Prioridad 3)
│   ├── ActionIsSleepy
│   └── ActionSleep
├── SECUENCIA: Comer (Prioridad 4)
│   ├── ActionIsHungry
│   ├── ActionSearchFood
│   └── ActionEat
└── ActionWander (Por defecto)

VARIABLES CIERVO:
- hunger: 0-100 (aumenta 5 pts/seg)
- sleepiness: 0-100 (aumenta 3 pts/seg)
- normalSpeed: 3.5 m/s
- fleeSpeed: 7 m/s (cuando huye)

════════════════════════════════════════════════════════════════════════════════
                                    🐺 LOBO
════════════════════════════════════════════════════════════════════════════════

COMPORTAMIENTOS:
✅ Si tiene HAMBRE → Cazar ciervos
✅ Si tiene SUEÑO → Dormir
✅ EVITA pelear con Jabalís
✅ Si no tiene hambre ni sueño → Patrullar

ESTRUCTURA BEHAVIOR TREE LOBO:
ROOT (Selector)
├── SECUENCIA: Evitar Jabalí (Prioridad 1)
│   ├── ActionViewJabali
│   └── ActionAvoid
├── SECUENCIA: Dormir (Prioridad 2)
│   ├── ActionIsSleepy
│   └── ActionSleep
├── SECUENCIA: Cazar (Prioridad 3)
│   ├── ActionIsHungry
│   ├── ActionViewCiervo
│   └── ActionHuntCiervo
└── ActionWander (Por defecto)

VARIABLES LOBO:
- hunger: 0-100 (aumenta 8 pts/seg, más rápido que herbívoros)
- sleepiness: 0-100 (aumenta 4 pts/seg)
- normalSpeed: 4 m/s
- huntSpeed: 6 m/s (cuando caza)

════════════════════════════════════════════════════════════════════════════════
                                    🐗 JABALÍ
════════════════════════════════════════════════════════════════════════════════

COMPORTAMIENTOS:
✅ SIEMPRE permanece en su territorio (auto-retorno)
✅ Si tiene HAMBRE → Buscar hierba en su territorio
✅ Si tiene SUEÑO → Dormir
✅ ATACA cualquier intruso (Lobo o Ciervo) en su territorio
✅ Si no tiene hambre ni sueño → Patrullar territorio

ESTRUCTURA BEHAVIOR TREE JABALÍ:
ROOT (Selector)
├── SECUENCIA: Defender Territorio (Prioridad 1)
│   ├── ActionViewIntruder
│   └── ActionDefendTerritory
├── SECUENCIA: Dormir (Prioridad 2)
│   ├── ActionIsSleepy
│   └── ActionSleep
├── SECUENCIA: Comer (Prioridad 3)
│   ├── ActionIsHungry
│   ├── ActionSearchFood
│   └── ActionEat
└── ActionPatrolTerritory (Por defecto)

VARIABLES JABALÍ:
- hunger: 0-100 (aumenta 6 pts/seg)
- sleepiness: 0-100 (aumenta 3.5 pts/seg)
- territoryRadius: 15m (configurable)
- normalSpeed: 3 m/s
- aggressiveSpeed: 5 m/s (cuando ataca)

════════════════════════════════════════════════════════════════════════════════
                                ACCIONES DISPONIBLES
════════════════════════════════════════════════════════════════════════════════

CONDICIONALES:
✅ ActionIsHungry - Verifica si tiene hambre
✅ ActionIsSleepy - Verifica si tiene sueño

MOVIMIENTO:
✅ ActionSleep - Dormir hasta estar descansado
✅ ActionSearchFood - Buscar hierba cercana
✅ ActionEat - Comer hierba encontrada
✅ ActionRunAway - Huir de depredadores (Ciervo)
✅ ActionAvoid - Evitar enemigos (Ciervo y Lobo)
✅ ActionHuntCiervo - Cazar ciervos (Lobo)
✅ ActionPatrolTerritory - Patrullar territorio (Jabalí)
✅ ActionDefendTerritory - Atacar intrusos (Jabalí)
✅ ActionWander - Deambular por defecto

SENSORES:
✅ ActionViewLobo - Detectar lobos (Ciervo)
✅ ActionViewJabali - Detectar jabalís (Ciervo y Lobo)
✅ ActionViewCiervo - Detectar ciervos (Lobo)
✅ ActionViewIntruder - Detectar intrusos en territorio (Jabalí)

════════════════════════════════════════════════════════════════════════════════
                                  CONFIGURACIÓN
════════════════════════════════════════════════════════════════════════════════

TAGS REQUERIDOS:
- "Ciervo" para GameObjects de ciervos
- "Lobo" para GameObjects de lobos 
- "Jabali" para GameObjects de jabalís
- "Grass" o "Hierba" para objetos de comida

COMPONENTES REQUERIDOS:
- HealthCiervo en ciervos
- HealthLobo en lobos
- HealthJabali en jabalís
- NavMeshAgent en todos
- BehaviorTree en todos

PASOS DE INSTALACIÓN:
1. 🔄 Recompilar Unity (Ctrl+R)
2. 🏷️ Asignar tags correctos a GameObjects
3. 🌳 Configurar Behavior Trees según estructuras arriba
4. 🎯 Reemplazar nodos "Unknown Action" 
5. 🐾 Ajustar territoryRadius del Jabalí en inspector
6. ▶️ Probar en Play Mode

════════════════════════════════════════════════════════════════════════════════
                                LOGS ESPERADOS
════════════════════════════════════════════════════════════════════════════════

CIERVO:
🦌 "NORMAL | Hambre: 25.0/100 | Sueño: 30.0/100"
🍽️ "¿Tiene hambre? True (Hambre: 35.0)"
🔍 "INICIANDO BÚSQUEDA DE COMIDA"
🌿 "Comiendo hierba... Hambre: 15.0"
🐺 "¡LOBO DETECTADO! Distancia: 8.5m - ACTIVANDO HUIDA URGENTE"
🚨 "INICIANDO HUIDA URGENTE!"

LOBO:
🐺 "CAZANDO | Hambre: 45.0/100 | Sueño: 20.0/100"
🎯 "¡CIERVO VIVO DETECTADO! Distancia: 12.3m"
🎯 "INICIANDO CAZA DE CIERVO"
🍖 "¡ATRAPÓ CIERVO! Comiendo..."

JABALÍ:
🐗 "PATRULLANDO | Hambre: 20.0/100 | Sueño: 25.0/100 | Territorio: 5.2m/15m"
🚨 "¡INTRUSO DETECTADO EN TERRITORIO!"
⚔️ "INICIANDO DEFENSA DEL TERRITORIO"
⚔️ "ATACANDO a Lobo_01!"

════════════════════════════════════════════════════════════════════════════════
                                  DEBUGGING
════════════════════════════════════════════════════════════════════════════════

SCRIPTS DE DEBUG DISPONIBLES:
📋 DiagnosticoCiervo.cs - Diagnóstico completo del Ciervo
📋 DebugBehaviorTree.cs - Estado general del Behavior Tree
📋 PruebaEstamina.cs - Herramientas para probar necesidades manualmente

COMANDOS ÚTILES EN INSPECTOR:
- "Ejecutar Diagnóstico Inmediato"
- "Agotar Estamina" → "Agotar Hambre"
- "Estamina Completa" → "Satisfacer Hambre"
- "Mostrar Estado Actual"

════════════════════════════════════════════════════════════════════════════════

¡El nuevo sistema está diseñado para ser mucho más realista y específico para cada tipo de animal!
*/
