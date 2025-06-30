# Lista de Componentes Necesarios para el NPC Ciervo

## Componentes OBLIGATORIOS:

1. **HealthCiervo** ✅ (ya lo tienes)
   - Type Agent: Ciervo
   - Stamina configurada

2. **VisionSensor** 
   - Para detectar Lobos y Jabalíes
   - Configurar Enemy View

3. **AICharacterVehicleCiervo**
   - Para movimiento (correr, huir, deambular)
   - NavMeshAgent incluido

4. **AICharacterActionCiervo**
   - Para acciones (descansar, regenerar estamina)

5. **BehaviorTree**
   - Asignar el árbol de comportamiento del Ciervo
   - External Behavior: Tu asset del árbol del Ciervo

6. **NavMeshAgent**
   - Speed: 6-8 (rápido para huir)
   - Stopping Distance: 1.5
   - Auto Repath: true

## Configuraciones Importantes:

### HealthCiervo:
- Type Agent: Ciervo ⚠️ CRÍTICO
- Health: 80
- Stamina: 100
- Stamina Depletion Rate: 8
- Stamina Regen Rate: 15

### VisionSensor:
- Distance: 15-20 (buena vista)
- Angle: 60-90 grados
- Detectar: Lobo, Jabali

### NavMeshAgent:
- Speed: 7 (ágil)
- Angular Speed: 180
- Acceleration: 12
