/*
	Copyright 2011-2019 Daniel S. Buckstein

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
*/

/*
	animal3D SDK: Minimal 3D Animation Framework
	By Daniel S. Buckstein
	
	passTexcoord_transform_vs4x.glsl
	Transform position and texcoord attributes, pass results along.
*/

#version 410

// **INSTRUCTIONS: 
//	1) declare uniform variable for MVP matrix; see demo code for hint
//	2) correctly transform input position by MVP matrix, store as output
//	3) declare attribute for texture coordinate
//		-> look in 'a3_VertexDescriptors.h' for the correct location index
//	4) declare uniform for atlas matrix
//	5) declare varying for transformed texture coordinate
//	6) transform texture coordinate attribute and copy to varying

layout (location = 0) in vec4 aPosition;
layout (location = 8) in vec4 texCoord;
uniform mat4 uMVP; //1
uniform mat4 uAtlas; //4

out vec4 temp; //5

void main()
{
	temp = texCoord * uAtlas; //6

	// DUMMY OUTPUT: directly assign input position to output position
	gl_Position = aPosition * uMVP; //2
}
