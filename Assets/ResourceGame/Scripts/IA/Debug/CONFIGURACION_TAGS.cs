/*
 * CONFIGURACIÓN DE TAGS REQUERIDOS PARA EL NUEVO SISTEMA
 * ====================================================
 * 
 * ERROR DETECTADO:
 * - UnityException: Tag: Grass is not defined
 * - El sistema busca objetos con tag "Grass" para que los animales coman
 * 
 * TAGS NECESARIOS PARA EL NUEVO SISTEMA:
 * 
 * 1. COMIDA:
 *    - "Grass" - Para hierba/césped que comen Ciervo y Jabalí
 *    - "Hierba" - Alternativo para hierba (ya implementado en código)
 *    - "Food" - Para comida general (opcional)
 * 
 * 2. ANIMALES (si no existen):
 *    - "Ciervo" - Para identificar ciervos
 *    - "Lobo" - Para identificar lobos  
 *    - "Jabali" - Para identificar jabalíes
 * 
 * CÓMO CREAR TAGS EN UNITY:
 * 
 * 1. Ve a: Edit → Project Settings → Tags and Layers
 * 2. En sección "Tags", click en "+"
 * 3. Agrega estos tags:
 *    - Grass
 *    - Hierba (si no existe)
 *    - Ciervo (si no existe)
 *    - Lobo (si no existe)
 *    - Jabali (si no existe)
 * 
 * ASIGNAR TAGS A OBJETOS:
 * 
 * 1. Selecciona objetos de césped/hierba en la escena
 * 2. En Inspector, campo "Tag" → selecciona "Grass" o "Hierba"
 * 3. Para animales, asigna sus respectivos tags
 * 
 * CONFIGURACIÓN ALTERNATIVA:
 * 
 * Si ya tienes objetos de comida con otros tags, 
 * puedes modificar ActionSearchFood.cs para usar esos tags.
 * 
 * OBJETOS REQUERIDOS EN LA ESCENA:
 * 
 * - Varios objetos con tag "Grass" distribuidos por el mapa
 * - Deben tener Colliders para detectar colisiones
 * - Recomendado: 10-20 objetos de comida repartidos
 * 
 * ESTADO ACTUAL:
 * ✅ Sistema AI funcionando correctamente
 * ✅ Behavior Trees configurados
 * ✅ Código sin errores de compilación
 * 🔧 Solo falta configurar tags y objetos de comida
 */
