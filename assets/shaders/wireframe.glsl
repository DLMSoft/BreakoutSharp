/**
 * wireframe.glsl
 * A simple shader for wireframe mode.
 */
#ifdef COMPILE_VERT
layout (location = 0) in vec2 a_Pos;

uniform vec2 u_Resolution;
uniform mat4 u_Model;

void main() {
    vec2 transformated = (u_Model * vec4(a_Pos, 0.0, 1.0)).xy;

    vec2 halfRes = u_Resolution / 2;

    vec2 finalPos = transformated / halfRes - 1.0;
    
    gl_Position = vec4(finalPos.x, -finalPos.y, 0, 1);
}
#endif

#ifdef COMPILE_FRAG
out vec4 o_FragColor;

void main() {
    o_FragColor = vec4(1.0, 0.0, 0.0, 1.0);
}
#endif