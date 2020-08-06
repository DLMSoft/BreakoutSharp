#version 330 core
/**
 * sprite.glsl
 * Standard shader for 2D sprite.
 */
#ifdef COMPILE_VERT
layout (location = 0) in vec2 a_Pos;
layout (location = 1) in vec2 a_TexCoord;

out vec2 v_TexCoord;

uniform vec2 u_Resolution;
uniform mat4 u_Model;

void main() {
    vec2 transformated = (vec4(a_Pos, 0.0, 0.0) * u_Model).xy;

    vec4 finalPos = transformated / (u_Resolution / 2.0) - 1.0;
    
    v_TexCoord = a_TexCoord;

    gl_Position = vec4(finalPos.x, -finalPos.y, 0, 0);
}
#endif

#ifdef COMPILE_FRAG
out vec4 o_FragColor;

in vec2 v_TexCoord;

uniform vec4 u_Color;
uniform float u_Opacity;

uniform sampler2D u_Texture;

void main() {
    if (u_Color.w <= 0.0 || u_Opacity <= 0.0) {
        o_FragColor = vec4(0.0, 0.0, 0.0, 0.0);
        return;
    }

    vec4 result = texture(u_Texture, v_TexCoord) * vec4(u_Color.xyz, u_Color.w * u_Opacity);

    o_FragColor = result;
}
#endif