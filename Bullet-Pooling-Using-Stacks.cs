/*
* This incomplete class provides an example of how to create an object pool using C#'s stack feature
* It might not be the most efficient method but it has been good enough for my needs and is easy to work with. 
*/

public static Stack<GameObject> redBulletStack;// = new Stack<GameObject>(96);

 Awake() {
for (int i=0; i<96; i++) { //create all the bullets we'll ever want ever
redBullets[i] = Instantiate(redBullet, new Vector3(1000,0,1000), Quaternion.identity) as GameObject;
redBullets[i].transform.parent = parent;
}}

redBulletStack = new Stack<GameObject>(redBullets);

//then do something like this when you want to fire a bullet

bullet = Spawn.redBulletStack.Pop();
bullet.transform.parent = null;

bullet.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
Bullet bulletScript = bullet.GetComponent<Bullet>();
bullet.renderer.enabled = true; //all these would be false by default
bullet.collider.enabled = true;
bulletScript.enabled = true;

//finally when you catch the bullets, instead of destroying them:

void CleanUp() {
Spawn.redBulletStack.Push(gameObject);
//transform.position = new Vector3(1000,0,1000);
transform.parent = GameObject.FindGameObjectWithTag("bulletParent").transform;
renderer.enabled = false;
collider.enabled = false;
enabled = false;
}
