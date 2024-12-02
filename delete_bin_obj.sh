#!/bin/bash

# Elimina las carpetas 'bin' y 'obj' en el directorio actual y sus subdirectorios

# Obtiene el directorio desde el cual se ejecuta el script
base_path="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

# Si deseas usar el directorio actual de ejecución en lugar del directorio del script, descomenta la siguiente línea:
# base_path="$(pwd)"

# Verifica si la ruta existe
if [ ! -d "$base_path" ]; then
    echo "La ruta especificada no existe: $base_path"
    exit 1
fi

# Encuentra y elimina todas las carpetas 'bin' y 'obj'
find "$base_path" -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

echo "Proceso completado."
